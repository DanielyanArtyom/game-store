
using GameStore.Mongo.Data.Context.Entity;
using MongoOrder = GameStore.Mongo.Data.Context.Entity.Order;
using Order = GameStore.Data.Context.Entity.Order;

namespace GameStore.Business.Service;

public class SyncService : ISyncService
{
    private readonly IMongoUnitOfWork _mongoUnitOfWork;
    private readonly IUnitOfWork _sqlUnitOfWork;
    private readonly IPublisherService _publisherService;
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;
    
    public SyncService(
        IMongoUnitOfWork unitOfWork, 
        IUnitOfWork sqlUnitOfWork, 
        IPublisherService publisherService,
        IMapper mapper,
        IGenreService genreService
        )
    {
        _mongoUnitOfWork = unitOfWork;
        _sqlUnitOfWork = sqlUnitOfWork;
        _mapper = mapper;
        _publisherService = publisherService;
        _genreService = genreService;
    }
    
    public async Task<Game?> SyncProductAsync(Product product, CancellationToken ct = default)
    {
        var publisher = SyncSuppliersWithSqlAsync(product.SupplierID, ct);
        var genre = SyncCategoriesWithSqlAsync(product.CategoryID, ct);

        await Task.WhenAll(publisher, genre);

        var newGame = _mapper.Map<Game>(product);
        newGame.PublisherId = publisher.Result.Id;
        
        newGame.GameGenres = new List<GameGenre>
        {
            new GameGenre { GenreId = genre.Result.Id }
        };

        // Put random Platform, as I do not have any info in MongoDb about it
        newGame.GamePlatforms = new List<GamePlatform>
        {
            new GamePlatform { PlatformId = new Guid("01f73b48-bf3e-45df-a107-c65f302ce8fa") }
        };

        _sqlUnitOfWork.Games.Add(newGame);

        await _sqlUnitOfWork.CompleteAsync(ct);

        return newGame;
    }

    public async Task<Order?> SyncOrderAsync(MongoOrder order, Guid customerId ,CancellationToken ct = default)
    {
        // Put constant customerId , because do not have any customer models yet!
        var newOrder = new Order
        {
            Date = DateTime.Parse(order.OrderDate),
            CustomerId = customerId,
            Status = DateTime.Parse(order.ShippedDate) > DateTime.UtcNow ? OrderStatus.Open : OrderStatus.Paid,
            OriginalId = order.OrderID,
            OrderGames = new()
        };
        
        _sqlUnitOfWork.Orders.Add(newOrder);

        await _sqlUnitOfWork.CompleteAsync(ct);
        return newOrder;
    }

    private async Task<Publisher> SyncSuppliersWithSqlAsync(int supplierId, CancellationToken ct = default)
    {
         var supplier = (await _mongoUnitOfWork.Suppliers.SearchAsync(new SearchContext<Supplier>
         {
             Filter = x => x.SupplierID == supplierId
         }, ct)).Results.FirstOrDefault();
        
        if (supplier == null)
        {
            throw new NotFoundException("Supplier not Found");
        }
        
        var existingPublisher = (await _publisherService.GetByCompanyNameAsync(supplier.CompanyName));

        if (existingPublisher != null)
        {
            return _mapper.Map<Publisher>(existingPublisher);
        }

        var publisher = _mapper.Map<Publisher>(supplier);

        _sqlUnitOfWork.Publishers.Add(publisher);
        
        await _sqlUnitOfWork.CompleteAsync(ct);
        
        return publisher;
    }
    
    private async Task<Genre> SyncCategoriesWithSqlAsync(int categoryId, CancellationToken ct = default)
    {
        var category = (await _mongoUnitOfWork.Categories.SearchAsync(new SearchContext<Category>
        {
            Filter = x => x.CategoryID == categoryId
        }, ct)).Results.FirstOrDefault();

        if (category == null)
        {
            throw new NotFoundException("Category not Found");
        }
        
        var existingGenre = (await _genreService.GetByGenreName(category.CategoryName));

        if (existingGenre != null)
        {
            return _mapper.Map<Genre>(existingGenre);
        }

        var genre = _mapper.Map<Genre>(category);

        _sqlUnitOfWork.Genres.Add(genre);
        
        await _sqlUnitOfWork.CompleteAsync(ct);
        
        return genre;
    }

}
