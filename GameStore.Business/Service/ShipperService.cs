using GameStore.Mongo.Data.Context.Entity;

namespace GameStore.Business.Service;

public class ShipperService: IShipperService
{
    private readonly IMongoUnitOfWork _noSqlUnitOfWork;
    private readonly IMapper _mapper;
    
    public ShipperService(IMongoUnitOfWork noSqlUnitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _noSqlUnitOfWork = noSqlUnitOfWork;
    }

    public async Task<List<ShipperModel>> GetShippers()
    {
        var shippers = await _noSqlUnitOfWork.Shippers.SearchAsync(new SearchContext<Shipper>());
        
        return _mapper.Map<List<ShipperModel>>(shippers.Results);
    } 
}