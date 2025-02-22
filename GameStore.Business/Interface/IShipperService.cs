using GameStore.Mongo.Data.Context.Entity;

namespace GameStore.Business.Interface;

public interface IShipperService
{
    Task<List<ShipperModel>> GetShippers();
}