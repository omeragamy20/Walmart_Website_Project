using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Payment;
using System.Threading.Tasks;
using Ecommerce.Application.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ecommerce.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return View(payments);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Create_DTO paymentDto)
        {
            if (ModelState.IsValid)
            {
                await _paymentService.AddPaymentAsync(paymentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(paymentDto);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var payment = await _paymentService.GetOneAsync(id); // Assumed method to get payment by ID
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Create_DTO paymentDto)
        {
            if (ModelState.IsValid)
            {
                await _paymentService.UpdatePaymentAsync(paymentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(paymentDto);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _paymentService.GetOneAsync(id); // Assumed method to get payment by ID
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int  id)
        {
            var payment = await _paymentService.GetOneAsync(id);
            await _paymentService.DeletePaymentAsync(payment.Data); // Assumed method to delete payment by ID
            return RedirectToAction(nameof(Index));
        }
    }
}

