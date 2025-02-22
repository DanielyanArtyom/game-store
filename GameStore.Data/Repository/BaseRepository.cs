using System.Linq.Expressions;
using GameStore.Data.Common.Search;

namespace GameStore.Data.Repository;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IDbContextFactory<GameStoreContext> _contextFactory;

    public BaseRepository(IDbContextFactory<GameStoreContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public void Add(T entity)
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            context.Set<T>().Add(entity);
        }
    }

    public void Update(Guid id, T entity)
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            context.Set<T>().Entry(entity).State = EntityState.Modified;
        }
    }
    
    public async Task<T> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        using (var context = await _contextFactory.CreateDbContextAsync(ct))
        {
            return await context.Set<T>().FindAsync(id);
        }
    }

    public void Delete(T entity)
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            context.Set<T>().Remove(entity);
        }
    }
    
    public void DeleteById(Guid id)
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            context.Set<T>().Where(x => x.Id == id).ExecuteDelete();
        }
    }

    public async Task<int> GetTotalCountAsync(CancellationToken ct = default)
    {
        using (var context = await _contextFactory.CreateDbContextAsync(ct))
        {
            return await context.Set<T>().CountAsync(ct);
        }
    }
    
    public async Task<int> GetFilteredCountAsync(Expression<Func<T, bool>>? filter, CancellationToken ct = default)
    {
        using (var dbContext = await _contextFactory.CreateDbContextAsync(ct))
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync(ct);
        }
    }

    public async Task<SearchResult<T>> SearchAsync(SearchContext<T> context, CancellationToken ct = default)
    {
        using (var dbContext = await _contextFactory.CreateDbContextAsync(ct))
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (context.Filter != null)
            {
                query = query.Where(context.Filter);
            }

            if (context.Include != null)
            {
                foreach (var includeExpression in context.Include)
                {
                    query = query.Include(includeExpression);
                }
            }

            int totalCount = await query.CountAsync(ct);

            if (context.OrderBy != null)
            {
                query = context.IsAscending
                    ? query.OrderBy(context.OrderBy)
                    : query.OrderByDescending(context.OrderBy);
            }

            if (context.PageSize < int.MaxValue)
            {
                var skip = (context.PageNumber - 1) * context.PageSize;
                query = query.Skip(skip).Take(context.PageSize);
            }

            var results = await query.ToListAsync(ct);

            return new SearchResult<T>
            {
                TotalCount = totalCount,
                Results = results
            };
        }
    } 
}
