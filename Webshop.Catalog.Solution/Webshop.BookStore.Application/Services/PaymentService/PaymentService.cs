using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly ILogger<PaymentService> _logger;

    private readonly HttpClient _httpClient;

    public PaymentService(IHttpClientFactory httpClientFactory, ILogger<PaymentService> _logger)
    {
        this._logger = _logger;
        _httpClient = httpClientFactory.CreateClient("PaymentServiceClient");
    }

    public async Task<Result> ProcessPayment(PaymentRequest paymentRequest)
    {
        string url = "process";

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, paymentRequest);
            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail(Errors.General.UnspecifiedError($"Error: An error occurred while processing payment. Status code: {response.StatusCode}"));
            }

            return Result.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while calling the payment service.");
            throw;
        }
    }
}