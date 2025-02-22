
namespace GameStore.Business.Service;

public class BankPaymentService
{
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PaymentServiceOptions _paymentOptions;
    
    public BankPaymentService(IFileService fileService, IUnitOfWork unitOfWork, PaymentServiceOptions paymentOptions)
    {
        _fileService = fileService;
        _unitOfWork = unitOfWork;
        _paymentOptions = paymentOptions;
    }
    
    public async Task<byte[]> ProcessPaymentAsync(Guid cusomterId ,CancellationToken ct = default)
    {
        var order = (await _unitOfWork.Orders.SearchAsync(new SearchContext<Order>
        {
            Filter = x => x.Status == OrderStatus.Open && x.CustomerId == cusomterId ,  
            Include = new Expression<Func<Order, object>>[] { x => x.OrderGames }
        })).Results.FirstOrDefault();

        if (order == null)
        {
            throw new NotFoundException("Order not found!");
        }

        var pdfInvoice = await _fileService.GenerateInvoicePdfBytes(new GeneratePdfInvoiceContext
        {
            UserId = cusomterId,
            OrderId = order.Id,
            Sum = order.TotalSum,
            ExpireAfterMonths = _paymentOptions.OrderValidityInMonths,
        });

        order.Status = OrderStatus.Paid;
        
        _unitOfWork.Orders.Update(order.Id, order);

        await _unitOfWork.CompleteAsync(ct);

        return pdfInvoice;
    }


}