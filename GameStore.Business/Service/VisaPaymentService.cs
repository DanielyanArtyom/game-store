namespace GameStore.Business.Service;

public class VisaPaymentService
{
    private readonly IPaymentClient _paymentClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVisitor _visitor;
    private readonly IRevertRepository _revertRepository;
    
    public VisaPaymentService(IPaymentClient paymentClient, IRevertRepository revertRepository, IUnitOfWork unitOfWork, IVisitor visitor )
    {
        _paymentClient = paymentClient;
        _unitOfWork = unitOfWork;
        _visitor = visitor;
        _revertRepository = revertRepository;
    }
    
    public async Task ProcessPaymentAsync(PaymentCardModel request, Guid customerId ,CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var order = (await _unitOfWork.Orders.SearchAsync(new SearchContext<Order>
            { Filter = x => x.Status == OrderStatus.Open && x.CustomerId == customerId ,  Include = new Expression<Func<Order, object>>[] { x => x.OrderGames }}, ct)).Results.FirstOrDefault();

        if (order == null)
        {
            throw new NotFoundException("Order not found!");
        }

        try
        {
            await _paymentClient.VisaPaymentAsync(new VisaRequest
            {
                TransactionAmount = order.TotalSum,
                CardHolderName = request.Holder,
                CardNumber = request.CardNumber,
                ExpirationMonth = request.MonthExpire,
                ExpirationYear = request.YearExpire,
                Cvv = request.CVV2
            }, ct);

            _unitOfWork.Orders.Update(order.Id, order);

            await _unitOfWork.CompleteAsync(ct);
        }
        catch
        {
            await _revertRepository.ResetCancelledOrderAsync(order, ct);
            throw;
        }
    }
}