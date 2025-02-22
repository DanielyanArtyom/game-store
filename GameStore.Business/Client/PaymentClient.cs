using System.Text;
using Newtonsoft.Json;

namespace GameStore.Business.Client;

internal class PaymentClient : IPaymentClient
{
    private readonly HttpClient _httpClient;
    private readonly PaymentServiceOptions _options;

    public PaymentClient(HttpClient httpClient, PaymentServiceOptions paymentOptions)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(paymentOptions.PaymentServiceUrl);
        _options = paymentOptions;
    }

    public async Task BoxPaymentAsync(IBoxRequest request, CancellationToken cancellationToken = default)
    {
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_options.IBoxUrl, content, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task VisaPaymentAsync(VisaRequest request, CancellationToken cancellationToken)
    {
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_options.VisaUrl, content, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}

