using Ecommerce.DTOs.PayPalDTOs;
using Ecommerce.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PayPal.Api;

namespace Ecommerce.Application.Services
{
    public class PayPalService
    {

        //private readonly PayPalHttpClient _client;


        //private readonly IConfiguration configuration;

        //public PayPalService(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}

        //public Payment CreatePayment(IEnumerable<ItemDto> items, string baseUrl)
        //{
        //    var subtotal = 0M;

        //    var itemList = new ItemList
        //    {
        //        items = items.Select(x =>
        //        {
        //            subtotal += x.Price * x.Quantity;
        //            return new PayPal.Api.Item
        //            {
        //                name = x.Name,
        //                currency = "USD",
        //                price = x.Price.ToString(),
        //                quantity = x.Quantity.ToString()
        //            };
        //        }).ToList()
        //    };

        //    var shipping = 0M;
        //    var tax = 0M;

        //    var transactions = new List<Transaction>
        //    {
        //        new()
        //        {
        //            description = "Shopping Cart purchase",
        //            item_list = itemList,
        //            amount = new()
        //            {
        //                currency = "USD",
        //                details = new()
        //                {
        //                    shipping = shipping.ToString(),
        //                    tax = tax.ToString(),
        //                    subtotal = subtotal.ToString()
        //                },
        //                total = (shipping + tax + subtotal).ToString()
        //            }
        //        }
        //    };

        //    var payment = new Payment
        //    {
        //        intent = "sale",
        //        payer = new PayPal.Api.Payer { payment_method = "paypal" },
        //        transactions = transactions,
        //        redirect_urls = new()
        //        {
        //            cancel_url = "/",
        //            return_url = $"/{baseUrl}/ExecutePayment"
        //        }
        //    };

        //    return payment.Create(GetContext());
        //}

        //public Payment ExecutePayment(ExecutePaymentDto dto)
        //{
        //    var paymentExecution = new PaymentExecution { payer_id = dto.PayerId };
        //    var payment = new Payment { id = dto.PaymentId };

        //    return payment.Execute(GetContext(), paymentExecution);
        //}

        //private APIContext GetContext() =>
        //    new(GetAccessToken()) { Config = GetConfig() };
        //private string GetAccessToken() => new OAuthTokenCredential(GetConfig()).GetAccessToken();

        //private Dictionary<string, string> GetConfig() =>
        //    new()
        //    {
        //        { "mode", configuration["PayPal:Mode"] },
        //        { "clientId", configuration["PayPal:ClientId"] },
        //        { "clientSecret", configuration["PayPal:ClientSecret"] },
        //    };






        //public PayPalService(IConfiguration configuration)
        //{
        //    // Use either SandboxEnvironment or LiveEnvironment based on configuration
        //    var environment = new SandboxEnvironment(
        //        configuration["PayPal:ClientId"],
        //        configuration["PayPal:Secret"]);
        //    _client = new PayPalHttpClient(environment);
        //}

        //public async Task<SelectOrderBDTO> CaptureOrderAsync(string orderId)
        //{
        //    var request = new OrdersCaptureRequest(orderId);
        //    request.RequestBody(new OrderActionRequest());

        //    try
        //    {
        //        var response = await _client.Execute(request);
        //        return response.Result<SelectOrderBDTO>();
        //    }
        //    catch (HttpException ex)
        //    {
        //        // Handle error response
        //        Console.WriteLine($"PayPal capture failed: {ex.Message}");
        //        throw;
        //    }
        //}
        //////////////////////////////////////////////

        private readonly IConfiguration configuration;

        public PayPalService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public PayPal.Api.Payment CreatePayment(IEnumerable<ItemDto> items, string baseUrl)
        {
            var subtotal = 0M;

            var itemList = new PayPal.Api.ItemList
            {
                items = items.Select(x =>
                {
                    subtotal += x.Price * x.Quantity;
                    return new PayPal.Api.Item
                    {
                        name = x.Name,
                        currency = "USD",
                        price = x.Price.ToString(),
                        quantity = x.Quantity.ToString()
                    };
                }).ToList()
            };

            var shipping = 0M;
            var tax = 0M;

            var transactions = new List<PayPal.Api.Transaction>
            {
                new()
                {
                    description = "Shopping Cart purchase",
                    item_list = itemList,
                    amount = new()
                    {
                        currency = "USD",
                        details = new()
                        {
                            shipping = shipping.ToString(),
                            tax = tax.ToString(),
                            subtotal = subtotal.ToString()
                        },
                        total = (shipping + tax + subtotal).ToString()
                    }
                }
            };

            var payment = new PayPal.Api.Payment
            {
                intent = "sale",
                payer = new PayPal.Api.Payer { payment_method = "paypal" },
                transactions = transactions,
                redirect_urls = new()
                {
                    cancel_url = "/",
                    return_url = $"/{baseUrl}/ExecutePayment"
                }
            };

            return payment.Create(GetContext());
        }

        public PayPal.Api.Payment ExecutePayment(ExecutePaymentDto dto)
        {
            var paymentExecution = new PayPal.Api.PaymentExecution { payer_id = dto.PayerId };
            var payment = new PayPal.Api.Payment { id = dto.PaymentId };

            return payment.Execute(GetContext(), paymentExecution);
        }

        private PayPal.Api.APIContext GetContext() =>
            new(GetAccessToken()) { Config = GetConfig() };
        private string GetAccessToken() => new PayPal.Api.OAuthTokenCredential(GetConfig()).GetAccessToken();

        private Dictionary<string, string> GetConfig() =>
            new()
            {
                { "mode", configuration["PayPal:Mode"] },
                { "clientId", configuration["PayPal:ClientId"] },
                { "clientSecret", configuration["PayPal:ClientSecret"] },
            };
    }
}
