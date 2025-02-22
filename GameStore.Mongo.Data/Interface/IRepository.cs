using GameStore.Data.Common.Interface;

namespace GameStore.Mongo.Data.Interface;

public interface IRepository<T> : IBaseRepository<ObjectId, T> where T : class
{
}