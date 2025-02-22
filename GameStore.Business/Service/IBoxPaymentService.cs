namespace GameStore.Business.Service;

public class IBoxPaymentService
{
    private readonly IPaymentClient _paymentClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRevertRepository _revertRepository;
    
    public IBoxPaymentService(IPaymentClient paymentClient, IRevertRepository revertRepository ,IUnitOfWork unitOfWork )
    {
        _paymentClient = paymentClient;
        _unitOfWork = unitOfWork;
        _revertRepository = revertRepository;
    }
    
    public async Task<IBoxPaymentResultModel> ProcessPaymentAsync(Guid customerId ,CancellationToken ct = default)
    {
        var order = (await _unitOfWork.Orders.SearchAsync(new SearchContext<Order>
            { Filter = x => x.Status == OrderStatus.Open && x.CustomerId == customerId ,  Include = new Expression<Func<Order, object>>[] { x => x.OrderGames }}, ct)).Results.FirstOrDefault();

        if (order == null)
        {
            throw new NotFoundException("Order not found!");
        }

        try
        {
            await _paymentClient.BoxPaymentAsync(new IBoxRequest
            {
                AccountNumber = customerId,
                InvoiceNumber = order.Id,
                TransactionAmount = order.TotalSum
            }, ct);
        
            _unitOfWork.Orders.Update(order.Id, order);

            await _unitOfWork.CompleteAsync(ct);
      
            return new IBoxPaymentResultModel
            {
                UserId = customerId,
                OrderId = order.Id,
                PaymentDate = DateTime.UtcNow,
                Sum = order.TotalSum,
            };
        }
        catch
        {
            await _revertRepository.ResetCancelledOrderAsync(order, ct);
            throw;
        }
    }

}