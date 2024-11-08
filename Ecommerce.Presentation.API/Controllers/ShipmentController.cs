using Ecommerce.Application.Services;
using Ecommerce.DTOs.Shipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        // GET: api/Shipment
        [HttpGet("GetAllShipment")]
        public async Task<ActionResult> GetAll()
        {
            var shipments =  _shipmentService.GetAllShipmentsAsync().Result.ToList();
            if (shipments == null || shipments.Count == 0)
            {
                return NoContent();
            }
            return Ok(shipments);
        }

        // GET: api/Shipment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            if (shipment == null)
            {
                return NotFound("Shipment not found.");
            }
            return Ok(shipment);
        }

        // POST: api/Shipment
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDto shipmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newShipment = await _shipmentService.CreateShipmentAsync(shipmentDto);
            //return CreatedAtAction(nameof(GetById), new { id = newShipment.ZipCode }, newShipment);
            return Ok(newShipment);
        }

        // PUT: api/Shipment/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDTO shipmentDto)
        {
            if (shipmentDto == null || id != shipmentDto.Id)
            {
                return BadRequest("Invalid shipment data or ID mismatch.");
            }

            var updatedShipment = await _shipmentService.UpdateShipmentAsync(shipmentDto);
            if (updatedShipment == null)
            {
                return NotFound("Shipment not found.");
            }

            return Ok(updatedShipment);
        }

        // DELETE: api/Shipment/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _shipmentService.DeleteShipmentAsync(id);
            if (!deleted)
            {
                return NotFound("Shipment not found.");
            }

            return NoContent();
        }





    }
}
