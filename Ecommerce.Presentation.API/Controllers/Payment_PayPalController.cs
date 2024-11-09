using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using Ecommerce.DTOs.Payment;

namespace Ecommerce.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Payment_PayPalController : ControllerBase
    {

    }
}
