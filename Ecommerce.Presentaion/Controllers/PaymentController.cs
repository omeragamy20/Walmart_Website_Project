using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Payment;
using System.Threading.Tasks;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class PaymentController : Controller
    {
        private readonly Application.Services.IPaymentService _paymentService;

        public PaymentController(Application.Services.IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: Payment
        public async Task<IActionResult> Index(int id)
        {
            var pay = await _paymentService.GetPaymentByIdAsync(id);
            return View(pay);
        }

       
        // GET: Payment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateDtos paymentDtos)
        {
            if (ModelState.IsValid)
            {
                await _paymentService.CreatePaymentAsync(paymentDtos);
                return RedirectToAction("Index");
            }
            return View(paymentDtos);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _paymentService.GetPaymentByIdAsync(id);
            return View(data);
        }

        // POST: Payment/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateDTOs paymentDto)
        {
            if (paymentDto == null)
            {
                return BadRequest();
            }

            var updatedPayment = await _paymentService.UpdatePaymentAsync(paymentDto);

            if (updatedPayment != null)
            {
                return RedirectToAction("Index", "Payment");
            }
            return NotFound();
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _paymentService.DeletepaymentAsync(id);
            if (payment)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
       
    }
}
