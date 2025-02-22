using System.Linq.Expressions;

namespace GameStore.Business.Service;

public class PublisherService : IPublisherService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVisitor _visitor;

    public PublisherService(IUnitOfWork unitOfWork, IVisitor visitor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _visitor = visitor;
    }

    public async Task CreateAsync(PublisherModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);

        var existingPublisher = await _unitOfWork.Publishers.SearchAsync(new SearchContext<Publisher> { Filter = x => x.CompanyName == request.CompanyName }, ct);

        if (existingPublisher.TotalCount != 0)
        {
            throw new DuplicateFoundException("Publisher is Already exists");
        }

        var publisher = _mapper.Map<Publisher>(request);

        _unitOfWork.Publishers.Add(publisher);
        
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task UpdateAsync(PublisherModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);

        var existingPublisher = (await _unitOfWork.Publishers.SearchAsync(new SearchContext<Publisher> { Filter = x => x.CompanyName == request.CompanyName && x.Id != request.Id }, ct));

        if (existingPublisher.TotalCount != 0)
        {
            throw new DuplicateFoundException("Publisher is Already exists");
        }

        var publisher = _mapper.Map<Publisher>(request);

        _unitOfWork.Publishers.Update(publisher.Id, publisher);
        
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<PublisherModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var publisher = (await _unitOfWork.Publishers.SearchAsync(new SearchContext<Publisher> { Filter = x => x.Id == id }, ct)).Results.FirstOrDefault();

        return _mapper.Map<PublisherModel>(publisher);
    }
    
    public async Task<PublisherModel> GetByCompanyNameAsync(string companyName)
    {
        if (string.IsNullOrWhiteSpace(companyName))
        {
            throw new ArgumentException("Company Name is required and cannot be empty or whitespace.");
        }
        
        var publisher = (await _unitOfWork.Publishers.SearchAsync(new SearchContext<Publisher> { Filter = x => x.CompanyName == companyName })).Results.FirstOrDefault();

        return _mapper.Map<PublisherModel>(publisher);
    }

    public async Task<PublisherModel> GetByGameKeyAsync(string key)
    {
        var game = (await _unitOfWork.Games.SearchAsync(new SearchContext<Game>
        {
            Filter = x => x.Key == key,
            Include = new Expression<Func<Game, object>>[] { x => x.Publisher }
        })).Results.FirstOrDefault();

        if (game == null)
        {
            throw new NotFoundException("Game with this key was not found");
        }

        return _mapper.Map<PublisherModel>(game.Publisher);
    }

    public async Task<List<PublisherModel>> GetAllAsync()
    {
        var publishers = (await _unitOfWork.Publishers.SearchAsync(new SearchContext<Publisher>())).Results.ToList();

        return _mapper.Map<List<PublisherModel>>(publishers);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var publisher = (await _unitOfWork.Publishers.SearchAsync(new SearchContext<Publisher>{ Filter = x => x.Id == id }, ct)).Results.FirstOrDefault();

        if (publisher == null)
        {
            throw new NotFoundException("Publisher does not exists");
        }

        _unitOfWork.Publishers.Delete(publisher);
        await _unitOfWork.CompleteAsync(ct);
    }

  
}