#region old
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Runtime.Serialization;
//using System.Threading.Tasks;
//using _2B_Egypt.Application.DTOs.PaypalDTOs;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using PayPalCheckoutSdk.Orders;
//using PaypalServerSdk.Standard;
//using PaypalServerSdk.Standard.Authentication;
//using PaypalServerSdk.Standard.Controllers;
//using PaypalServerSdk.Standard.Http.Response;
//using PaypalServerSdk.Standard.Models;
//using AmountWithBreakdown = PaypalServerSdk.Standard.Models.AmountWithBreakdown;


////using PaypalServerSDK.Standard;
////using PaypalServerSDK.Standard.Authentication;
////using PaypalServerSDK.Standard.Controllers;
////using PaypalServerSDK.Standard.Http.Response;
////using PaypalServerSDK.Standard.Models;
//using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
//using Order = PaypalServerSdk.Standard.Models.Order;
//using OrderRequest = PaypalServerSdk.Standard.Models.OrderRequest;
//using PurchaseUnitRequest = PaypalServerSdk.Standard.Models.PurchaseUnitRequest;

//namespace PayPalAdvancedIntegration;
//[ApiController]
//public class CheckoutController : Controller
//{
//    private readonly OrdersController _ordersController;
//    private readonly PaymentsController _paymentsController;
//    private readonly Dictionary<string, CheckoutPaymentIntent> _paymentIntentMap;
//    private IConfiguration _configuration
//    {
//        get;
//    }
//    private string _paypalClientId
//    {
//        get
//        {
//            return System.Environment.GetEnvironmentVariable("ClientId");
//        }
//    }
//    private string _paypalClientSecret
//    {
//        get
//        {
//            return System.Environment.GetEnvironmentVariable("ClientSecret");
//        }
//    }

//    private readonly ILogger<CheckoutController> _logger;

//    public CheckoutController(IConfiguration configuration, ILogger<CheckoutController> logger)
//    {
//        _configuration = configuration;
//        _logger = logger;
//        _paymentIntentMap = new Dictionary<string, CheckoutPaymentIntent> {
//      {
//        "CAPTURE",
//        CheckoutPaymentIntent.Capture
//      },
//      {
//        "AUTHORIZE",
//        CheckoutPaymentIntent.Authorize
//      }
//    };

//        // Initialize the PayPal SDK client
//        PaypalServerSdkClient client = new PaypalServerSdkClient.Builder()
//          .Environment(PaypalServerSdk.Standard.Environment.Sandbox)
//          .ClientCredentialsAuth(
//            new ClientCredentialsAuthModel.Builder(
//            "ARFz2kmEa9965hDGm7H3VoZvhjTbJZzF4pPN1fPvGBVK7lCPA6Px3UQNLNkUQunevD9-X-HcWImvyq4Y",
//            "ELP7nRbmgK4vyBQL5JoWGVDu1Ba5mbHddepXGI8kgnxUJbT1aGZlBWK8R7UcNhhTsdcMMtyrceNyuop8").Build()
//          )
//          .LoggingConfig(config =>
//            config
//            .LogLevel(LogLevel.Information)
//            .RequestConfig(reqConfig => reqConfig.Body(true))
//            .ResponseConfig(respConfig => respConfig.Headers(true))
//          )
//          .Build();

//        _ordersController = client.OrdersController;
//        _paymentsController = client.PaymentsController;
//    }


//    //[HttpPost("api/orders")]
//    //public async Task<IActionResult> CreateOrder([FromBody] dynamic cart)
//    //{
//    //    try
//    //    {
//    //        var result = await _CreateOrder(cart);
//    //        return StatusCode((int)result.StatusCode, result.Data);
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        Console.Error.WriteLine("Failed to create order:", ex);
//    //        return StatusCode(500, new
//    //        {
//    //            error = "Failed to create order."
//    //        });
//    //    }
//    //}

//    //private async Task<dynamic> _CreateOrder(dynamic cart)
//    //{


//    //    OrdersCreateInput ordersCreateInput = new OrdersCreateInput
//    //    {
//    //        Body = new OrderRequest
//    //        {
//    //            Intent = _paymentIntentMap["CAPTURE"],
//    //            PurchaseUnits = new List<PurchaseUnitRequest> {
//    //        new PurchaseUnitRequest {
//    //          Amount = new AmountWithBreakdown {
//    //            CurrencyCode = "USD", MValue = cart.amount.ToString(),
//    //          },
//    //        },

//    //      },

//    //        },
//    //    };


//    //    ApiResponse<Order> result = await _ordersController.OrdersCreateAsync(ordersCreateInput);
//    //    return result;
//    //}

//    [HttpPost("api/Checkout")]
//    public async Task<IActionResult> CreateOrder([FromBody] CartPayPalDTO cart)
//    {
//        //var RES = JsonSerializer.Deserialize<CartResponseDTO>(cart);
//        try
//        {
//            // Extract the amount from the cart object
//            int amountValue = cart.quantity; // Assuming the amount is sent as a property named "Amount"

//            var result = await _CreateOrder(amountValue);
//            return StatusCode((int)result.StatusCode, result.Data);
//        }
//        catch (Exception ex)
//        {
//            Console.Error.WriteLine("Failed to create order:", ex);
//            return StatusCode(500, new
//            {
//                error = "Failed to create order."
//            });
//        }
//    }

//    private async Task<dynamic> _CreateOrder(int amountValue)
//    {
//        OrdersCreateInput ordersCreateInput = new OrdersCreateInput
//        {
//            Body = new OrderRequest
//            {
//                Intent = _paymentIntentMap["CAPTURE"],
//                PurchaseUnits = new List<PurchaseUnitRequest> {
//                new PurchaseUnitRequest {
//                    Amount = new AmountWithBreakdown {
//                        CurrencyCode = "USD",
//                        MValue = amountValue.ToString(), // Use the dynamic amount here
//                    },
//                },
//            },
//            },
//        };

//        ApiResponse<Order> result = await _ordersController.OrdersCreateAsync(ordersCreateInput);
//        return result;
//    }

//    [HttpPost("api/Checkout/{orderID}/capture")]
//    public async Task<IActionResult> CaptureOrder(string orderID)
//    {
//        try
//        {
//            var result = await _CaptureOrder(orderID);
//            return StatusCode((int)result.StatusCode, result.Data);
//        }
//        catch (Exception ex)
//        {
//            Console.Error.WriteLine("Failed to capture order:", ex);
//            return StatusCode(500, new
//            {
//                error = "Failed to capture order."
//            });
//        }
//    }

//    private async Task<dynamic> _CaptureOrder(string orderID)
//    {
//        OrdersCaptureInput ordersCaptureInput = new OrdersCaptureInput
//        {
//            Id = orderID,
//        };

//        ApiResponse<Order> result = await _ordersController.OrdersCaptureAsync(ordersCaptureInput);

//        return result;
//    }

//}
#endregion 


using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaypalServerSdk.Standard.Authentication;
using PaypalServerSdk.Standard.Controllers;
using PaypalServerSdk.Standard.Http.Response;
using PaypalServerSdk.Standard.Models;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Authentication;
using PaypalServerSdk.Standard.Controllers;
using PaypalServerSdk.Standard.Http.Response;
using PaypalServerSdk.Standard.Models;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
//using Order = PaypalServerSdk.Standard.Models.Order;
using Ecommerce.DTOs.shared;
using PayPal.Api;
using Ecommerce.DTOs.PayPalDTOs;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;







namespace PayPalAdvancedIntegration;

public class PayPalService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _environment;

    public PayPalService(IConfiguration configuration)
    {
        var paypalConfig = configuration.GetSection("PayPal");
        _clientId = paypalConfig["ClientId"];
        _clientSecret = paypalConfig["ClientSecret"];
        _environment = paypalConfig["Environment"];
    }

    public PayPalHttpClient CreateClient()
    {
        PayPalEnvironment environment = _environment == "sandbox"
            ? new SandboxEnvironment(_clientId, _clientSecret)
            : new LiveEnvironment(_clientId, _clientSecret);

        return new PayPalHttpClient(environment);
    }
}

//public class Program
//{
//    //public static void Main(string[] args)
//    //{
//    //    CreateHostBuilder(args).Build().Run();
//    //}

//    public static IHostBuilder CreateHostBuilder(string[] args) =>
//      Host.CreateDefaultBuilder(args)
//      .ConfigureWebHostDefaults(webBuilder =>
//      {
//          webBuilder.UseUrls("http://localhost:8080");
//          webBuilder.UseStartup<Startup>();
//      });
//}

//public class Startup
//{
//    public void ConfigureServices(IServiceCollection services)
//    {
//        services.AddMvc().AddNewtonsoftJson();
//        services.AddHttpClient();
//    }

//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }
//        app.UseRouting();
//        app.UseStaticFiles();
//        app.UseEndpoints(endpoints => {
//            endpoints.MapControllers();
//        });
//    }
//}

//[Route("api/[controller]")]
//[ApiController]
//public class CheckoutController : Controller
//{
//    #region Malik
//    //private readonly OrdersController _ordersController;
//    //private readonly PaymentsController _paymentsController;
//    //private readonly Dictionary<string, CheckoutPaymentIntent> _paymentIntentMap;
//    //private IConfiguration _configuration
//    //{
//    //    get;
//    //}
//    //private string _paypalClientId
//    //{
//    //    get
//    //    {
//    //        return System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID");
//    //    }
//    //}
//    //private string _paypalClientSecret
//    //{
//    //    get
//    //    {
//    //        return System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET");
//    //    }
//    //}

//    //private readonly ILogger<CheckoutController> _logger;

//    //public CheckoutController(IConfiguration configuration, ILogger<CheckoutController> logger)
//    //{
//    //    _configuration = configuration;
//    //    _logger = logger;
//    //    _paymentIntentMap = new Dictionary<string, CheckoutPaymentIntent> {
//    //        {
//    //            "CAPTURE",
//    //            CheckoutPaymentIntent.Capture
//    //        },
//    //        {
//    //            "AUTHORIZE",
//    //            CheckoutPaymentIntent.Authorize
//    //  }
//    //};

//    //    // Initialize the PayPal SDK client
//    //    PaypalServerSdkClient client = new PaypalServerSdkClient.Builder()
//    //      .Environment(PaypalServerSdk.Standard.Environment.Sandbox)
//    //      .ClientCredentialsAuth(
//    //        new ClientCredentialsAuthModel.Builder(
//    //                 "AVhyMOIeULOMnlO8t9LLikrCZ5BxmDIjFsI1LkzBbHyak0r4iyWm-9Z05VCYxdxXlIS9hJ9uUFst2X_d",
//    //                  "EN_YQrsWbLGMexwaMZrgRa9W-EViux2Sr_Ka6Plrz6ev_5iOwvbPW6kKoCrdqIqzNAZ6HQgedyKRSfP3").Build()
//    //      )
//    //      .LoggingConfig(config =>
//    //        config
//    //        .LogLevel(LogLevel.Information)
//    //        .RequestConfig(reqConfig => reqConfig.Body(true))
//    //        .ResponseConfig(respConfig => respConfig.Headers(true))
//    //      )
//    //      .Build();

//    //    _ordersController = client.OrdersController;
//    //    _paymentsController = client.PaymentsController;
//    //}


//    //[HttpPost("api/orders")]
//    //public async Task<IActionResult> CreateOrder([FromBody] CartPayPalDTO cart)
//    //{
//    //    try
//    //    {
//    //        var result = await _CreateOrder(cart);
//    //        return StatusCode((int)result.StatusCode, result.Data);
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        Console.Error.WriteLine("Failed to create order:", ex);
//    //        return StatusCode(500, new
//    //        {
//    //            error = "Failed to create order."
//    //        });
//    //    }
//    //}

//    //private async Task<dynamic> _CreateOrder(CartPayPalDTO cart)
//    //{


//    //    OrdersCreateInput ordersCreateInput = new OrdersCreateInput
//    //    {
//    //        Body = new OrderRequest
//    //        {
//    //            Intent = _paymentIntentMap["CAPTURE"],
//    //            PurchaseUnits = new List<PurchaseUnitRequest> {
//    //        new PurchaseUnitRequest {
//    //          Amount = new AmountWithBreakdown {
//    //            CurrencyCode = "USD",
//    //            MValue = cart.amount.ToString(),
//    //          },
//    //        },

//    //      },

//    //        },
//    //    };


//    //    ApiResponse<Order> result = await _ordersController.OrdersCreateAsync(ordersCreateInput);
//    //    return result;
//    //}



//    //[HttpPost("api/orders/capture/{orderID}")]
//    //public async Task<IActionResult> CaptureOrder(string orderID)
//    //{
//    //    try
//    //    {
//    //        var result = await _CaptureOrder(orderID);
//    //        return StatusCode((int)result.StatusCode, result.Data);
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        Console.Error.WriteLine("Failed to capture order:", ex);
//    //        return StatusCode(500, new
//    //        {
//    //            error = "Failed to capture order."
//    //        });
//    //    }
//    //}

//    //private async Task<dynamic> _CaptureOrder(string orderID)
//    //{
//    //    OrdersCaptureInput ordersCaptureInput = new OrdersCaptureInput
//    //    {
//    //        Id = orderID,
//    //    };

//    //    ApiResponse<Order> result = await _ordersController.OrdersCaptureAsync(ordersCaptureInput);

//    //    return result;
//    //}
//    #endregion

//    private readonly IConfiguration configuration;
//    //public CheckoutController(IConfiguration _configuration)
//    //{
//    //    configuration = _configuration;
//    //}

//        //public PayPalService(IConfiguration configuration)
//        //{
//        //    this.configuration = configuration;
//        //}
//    [HttpPost]
//    //public PayPal.Api.Payment CreatePayment(IEnumerable<ItemDto> items, string baseUrl)
//    public IActionResult CreatePayment(IEnumerable<ItemDto> items, string baseUrl)
//    {
//        var subtotal = 0M;

//        var itemList = new ItemList
//        {
//            items = items.Select(x =>
//            {
//                subtotal += x.Price * x.Quantity;
//                return new PayPal.Api.Item
//                {
//                    name = x.Name,
//                    currency = "USD",
//                    price = x.Price.ToString(),
//                    quantity = x.Quantity.ToString()
//                };
//            }).ToList()
//        };

//        var shipping = 0M;
//        var tax = 0M;

//        var transactions = new List<Transaction>
//        {
//            new()
//            {
//                description = "Shopping Cart purchase",
//                item_list = itemList,
//                amount = new()
//                {
//                    currency = "USD",
//                    details = new()
//                    {
//                        shipping = shipping.ToString(),
//                        tax = tax.ToString(),
//                        subtotal = subtotal.ToString()
//                    },
//                    total = (shipping + tax + subtotal).ToString()
//                }
//            }
//        };

//        var payment = new PayPal.Api.Payment
//        {
//            intent = "sale",
//            payer = new PayPal.Api.Payer { payment_method = "paypal" },
//            transactions = transactions,
//            redirect_urls = new()
//            {
//                cancel_url = "/",
//                return_url = $"/{baseUrl}/ExecutePayment"
//            }
//        };

//        return Ok(payment.Create(GetContext()));
//    }
//    [HttpPost]
//    //public PayPal.Api.Payment ExecutePayment(ExecutePaymentDto dto)
//    public IActionResult ExecutePayment(ExecutePaymentDto dto)
//    {
//        var paymentExecution = new PaymentExecution { payer_id = dto.PayerId };
//        var payment = new Payment { id = dto.PaymentId };

//        return Ok(payment.Execute(GetContext(), paymentExecution));
//    }

//    private APIContext GetContext() =>
//        new (GetAccessToken()) { Config = GetConfig() };
//    private string GetAccessToken() => new OAuthTokenCredential(GetConfig()).GetAccessToken();

//    private Dictionary<string, string> GetConfig() =>
//        new()
//        {
//            { "mode", configuration["PayPal:Mode"] },
//            { "clientId", configuration["PayPal:ClientId"] },
//            { "clientSecret", configuration["PayPal:ClientSecret"] },
//        };
//}



[Route("api/[controller]")]
[ApiController]
public class PayPalController : ControllerBase
{
    private readonly PayPalService _payPalService;

    public PayPalController(PayPalService payPalService)
    {
        _payPalService = payPalService;
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyPayment([FromBody] PaymentRequest request)
    {
        if (string.IsNullOrEmpty(request.OrderId))
        {
            return BadRequest(new { message = "Invalid order ID" });
        }

        try
        {
            // Call PayPal service to verify the order
            var client = _payPalService.CreateClient(); // Ensure this method exists in your PayPalService
            var requestOrder = new OrdersGetRequest(request.OrderId);
            var response = await client.Execute(requestOrder);
            var order = response.Result<PayPalCheckoutSdk.Orders.Order>();

            // Log the PayPal order response for debugging
            Console.WriteLine($"PayPal Order Response: {JsonConvert.SerializeObject(order)}");

            // Check the payment status
            if (order.Status == "COMPLETED")
            {
                // Payment is successful
                return Ok(new { message = "Payment verified successfully", orderDetails = order });
            }
            else
            {
                // Payment status isn't COMPLETED
                return BadRequest(new { message = $"Payment status is {order.Status}", orderDetails = order });
            }
        }
        catch (Exception ex)
        {
            // Handle errors and log them
            Console.WriteLine($"Error verifying PayPal order: {ex.Message}");
            return StatusCode(500, new { message = "An error occurred while verifying the payment", error = ex.Message });
        }
    }
    [HttpPost("capture-order")]
    public async Task<IActionResult> CaptureOrder([FromBody] string orderId)
    {
        try
        {
            var client = _payPalService.CreateClient();
            var request = new OrdersCaptureRequest(orderId);
            request.Prefer("return=representation");

            var response = await client.Execute(request);
            var capturedOrder = response.Result<PayPalCheckoutSdk.Orders.Order>();

            // Check if the order is successfully captured
            if (capturedOrder.Status == "COMPLETED")
            {
                return Ok(new { message = "Order captured successfully", order = capturedOrder });
            }
            else
            {
                return BadRequest(new { message = "Order capture failed", status = capturedOrder.Status });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error capturing order: {ex.Message}");
            //return StatusCode(500, new { message = "Error capturing order", error = ex.Message });
            return BadRequest(ex.Message );
        }
    }
    //[HttpPost("Authorize-Order")]
    //public async Task<IActionResult> AuthorizeOrder([FromBody] string orderId)
    //{
    //    try
    //    {
    //        var client = _payPalService.CreateClient();
    //        var request = new OrdersAuthorizeRequest(orderId);
    //        request.Prefer("return=representation");

    //        var response = await client.Execute(request);
    //        var orderDetails = response.Result<PayPalCheckoutSdk.Orders.Order>();

    //        // Check if the authorization was successful
    //        if (orderDetails.Status == "AUTHORIZED")
    //        {
    //            return Ok(new { message = "Order authorized successfully", order = orderDetails });
    //        }
    //        else
    //        {
    //            return BadRequest(new { message = "Order authorization failed", status = orderDetails.Status });
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error authorizing order: {ex.Message}");
    //        return StatusCode(500, new { message = "Error authorizing order", error = ex.Message });
    //    }
    //}

}

public class PaymentRequest
{
    public string OrderId { get; set; }
}

//public class CaptureOrderRequest
//{
//    public string OrderID { get; set; }
//}

//public class AccessTokenResponse
//{
//    public string AccessToken { get; set; }
//    public string TokenType { get; set; }
//    public int ExpiresIn { get; set; }
//}