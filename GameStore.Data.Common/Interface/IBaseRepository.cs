using GameStore.Data.Common.Search;

namespace GameStore.Data.Common.Interface;

public interface IBaseRepository<TKey, T> where T : class
{
    void Add(T entity);

    void Update(TKey id, T entity);

    void Delete(T entity);

    void DeleteById(TKey id);

    Task<T> GetByIdAsync(TKey id, CancellationToken ct = default);

    Task<int> GetTotalCountAsync(CancellationToken ct = default);

    Task<SearchResult<T>> SearchAsync(SearchContext<T> context, CancellationToken ct = default);
}