using MongoDB.Driver;
using GameStore.Mongo.Data.Interface;
using GameStore.Data.Common.Search;
using GameStore.Mongo.Data.Context.Entity;

namespace GameStore.Mongo.Data.Repository;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public BaseRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public void Add(T entity)
    {
        _collection.InsertOne(entity);
    }

    public void Update(ObjectId id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        _collection.ReplaceOne(filter, entity);
    }

    public void Delete(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.Id);
        _collection.DeleteOne(filter);
    }

    public void DeleteById(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        _collection.DeleteOne(filter);
    }

    public async Task<T> GetByIdAsync(ObjectId id, CancellationToken ct = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync(ct);
        return entity;
    }

    public async Task<int> GetTotalCountAsync(CancellationToken ct = default)
    {
        var count = await _collection.Find(Builders<T>.Filter.Empty).CountDocumentsAsync(ct);
        return (int)count;
    }

    public async Task<SearchResult<T>> SearchAsync(SearchContext<T> context, CancellationToken ct = default)
    {
        var filter = context.Filter != null
            ? Builders<T>.Filter.Where(context.Filter)
            : Builders<T>.Filter.Empty;

        int totalCount = (int)await _collection.CountDocumentsAsync(filter, cancellationToken: ct);

        SortDefinition<T> sort = null;
        
        if (context.OrderBy != null)
        {
            sort = context.IsAscending
                ? Builders<T>.Sort.Ascending(context.OrderBy)
                : Builders<T>.Sort.Descending(context.OrderBy);
        }

        var options = new FindOptions<T>
        {
            Skip = (context.PageNumber - 1) * context.PageSize,
            Limit = context.PageSize,
            Sort = sort
        };

        var cursor = await _collection.FindAsync(filter, options, ct);
        
        var results = await cursor.ToListAsync(ct);

        return new SearchResult<T>
        {
            TotalCount = totalCount,
            Results = results
        };
    }
}
