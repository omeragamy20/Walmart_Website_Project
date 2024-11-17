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
using Order = PaypalServerSdk.Standard.Models.Order;
using Ecommerce.DTOs.shared;

namespace PayPalAdvancedIntegration;

public class Program
{
    //public static void Main(string[] args)
    //{
    //    CreateHostBuilder(args).Build().Run();
    //}

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(webBuilder =>
      {
          webBuilder.UseUrls("http://localhost:8080");
          webBuilder.UseStartup<Startup>();
      });
}

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc().AddNewtonsoftJson();
        services.AddHttpClient();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseRouting();
        app.UseStaticFiles();
        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}

[ApiController]
public class CheckoutController : Controller
{
    private readonly OrdersController _ordersController;
    private readonly PaymentsController _paymentsController;
    private readonly Dictionary<string, CheckoutPaymentIntent> _paymentIntentMap;
    private IConfiguration _configuration
    {
        get;
    }
    private string _paypalClientId
    {
        get
        {
            return System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID");
        }
    }
    private string _paypalClientSecret
    {
        get
        {
            return System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET");
        }
    }

    private readonly ILogger<CheckoutController> _logger;

    public CheckoutController(IConfiguration configuration, ILogger<CheckoutController> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _paymentIntentMap = new Dictionary<string, CheckoutPaymentIntent> {
            {
                "CAPTURE",
                CheckoutPaymentIntent.Capture
            },
            {
                "AUTHORIZE",
                CheckoutPaymentIntent.Authorize
      }
    };

        // Initialize the PayPal SDK client
        PaypalServerSdkClient client = new PaypalServerSdkClient.Builder()
          .Environment(PaypalServerSdk.Standard.Environment.Sandbox)
          .ClientCredentialsAuth(
            new ClientCredentialsAuthModel.Builder(
                     "AVhyMOIeULOMnlO8t9LLikrCZ5BxmDIjFsI1LkzBbHyak0r4iyWm-9Z05VCYxdxXlIS9hJ9uUFst2X_d",
                      "EN_YQrsWbLGMexwaMZrgRa9W-EViux2Sr_Ka6Plrz6ev_5iOwvbPW6kKoCrdqIqzNAZ6HQgedyKRSfP3").Build()
          )
          .LoggingConfig(config =>
            config
            .LogLevel(LogLevel.Information)
            .RequestConfig(reqConfig => reqConfig.Body(true))
            .ResponseConfig(respConfig => respConfig.Headers(true))
          )
          .Build();

        _ordersController = client.OrdersController;
        _paymentsController = client.PaymentsController;
    }


    [HttpPost("api/orders")]
    public async Task<IActionResult> CreateOrder([FromBody] CartPayPalDTO cart)
    {
        try
        {
            var result = await _CreateOrder(cart);
            return StatusCode((int)result.StatusCode, result.Data);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Failed to create order:", ex);
            return StatusCode(500, new
            {
                error = "Failed to create order."
            });
        }
    }

    private async Task<dynamic> _CreateOrder(CartPayPalDTO cart)
    {


        OrdersCreateInput ordersCreateInput = new OrdersCreateInput
        {
            Body = new OrderRequest
            {
                Intent = _paymentIntentMap["CAPTURE"],
                PurchaseUnits = new List<PurchaseUnitRequest> {
            new PurchaseUnitRequest {
              Amount = new AmountWithBreakdown {
                CurrencyCode = "USD",
                MValue = cart.amount.ToString(),
              },
            },

          },

            },
        };


        ApiResponse<Order> result = await _ordersController.OrdersCreateAsync(ordersCreateInput);
        return result;
    }



    [HttpPost("api/orders/capture/{orderID}")]
    public async Task<IActionResult> CaptureOrder(string orderID)
    {
        try
        {
            var result = await _CaptureOrder(orderID);
            return StatusCode((int)result.StatusCode, result.Data);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Failed to capture order:", ex);
            return StatusCode(500, new
            {
                error = "Failed to capture order."
            });
        }
    }

    private async Task<dynamic> _CaptureOrder(string orderID)
    {
        OrdersCaptureInput ordersCaptureInput = new OrdersCaptureInput
        {
            Id = orderID,
        };

        ApiResponse<Order> result = await _ordersController.OrdersCaptureAsync(ordersCaptureInput);

        return result;
    }


}