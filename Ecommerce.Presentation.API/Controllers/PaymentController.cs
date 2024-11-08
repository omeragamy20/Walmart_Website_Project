using Ecommerce.Application.Services;
using Ecommerce.DTOs.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // This will apply authorization to all actions within this controller
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            if (payments == null || !payments.Any())
            {
                return NoContent();
            }
            return Ok(payments);
        }

        // GET: api/Payment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound("Payment not found.");
            }
            return Ok(payment);
        }

        // POST: api/Payment
        [HttpPost("Create")]
        public async Task<ActionResult> Create( CreateDtos paymentDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result=await _paymentService.CreatePaymentAsync(paymentDtos);
            //return CreatedAtAction(nameof(GetById), new { id = paymentDtos.Id }, paymentDtos);
            return Ok(result);
        }

        // PUT: api/Payment/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateDTOs paymentDtos)
        {
            if (paymentDtos == null || id != paymentDtos.Id)
            {
                return BadRequest("Invalid payment data or ID mismatch.");
            }

            var updatedPayment = await _paymentService.UpdatePaymentAsync(paymentDtos);

            if (updatedPayment == null)
            {
                return NotFound("Payment with the specified ID was not found.");
            }

            return Ok(updatedPayment);
        }

        // DELETE: api/Payment/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _paymentService.DeletepaymentAsync(id);
            if (!deleted)
            {
                return NotFound("Payment not found.");
            }

            return NoContent();
        }
    }
}
