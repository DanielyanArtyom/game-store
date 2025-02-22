using System.Linq.Expressions;
using GameStore.Data.Common.Interface;

namespace GameStore.Data.Interface;

public interface IRepository<T> : IBaseRepository<Guid, T> where T : BaseEntity
{
    Task<int> GetFilteredCountAsync(Expression<Func<T, bool>>? filter, CancellationToken ct = default);
}