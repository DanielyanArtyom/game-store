namespace GameStore.Business.DataManager;

public class TransactionsManager: ITransactionsManager
{
    private readonly IUnitOfWork _sqlUnitOfWork;
    
    public TransactionsManager(IUnitOfWork sqlUnitOfWork)
    {
        _sqlUnitOfWork = sqlUnitOfWork;
    }
    
    public Task BeginTransactionAsync(CancellationToken ct = default)
    {
        return _sqlUnitOfWork.BeginTransactionAsync(ct);

    }

    public Task CommitTransactionAsync(CancellationToken ct = default)
    {
        return _sqlUnitOfWork.CommitTransactionAsync(ct);
    }

    public Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        return _sqlUnitOfWork.RollbackTransactionAsync(ct);
    }
}