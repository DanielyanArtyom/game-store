namespace GameStore.Data.Common.Interface;

public interface IBaseUnitOfWork: IDisposable
{
    Task CompleteAsync(CancellationToken ct = default);
}