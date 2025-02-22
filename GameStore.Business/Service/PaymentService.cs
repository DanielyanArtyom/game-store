namespace GameStore.Business.Service;

public class PaymentService: IPaymentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PaymentMethodModel>> GetAllAsync()
    {
        var result = await _unitOfWork.PaymentMethods.SearchAsync(new SearchContext<PaymentMethod>());

        return _mapper.Map<List<PaymentMethodModel>>(result);
    }
}