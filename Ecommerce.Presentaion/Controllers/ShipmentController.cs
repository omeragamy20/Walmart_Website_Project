using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Shipment;
using System.Threading.Tasks;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ShipmentController : Controller
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        // GET: Shipment
        public async Task<IActionResult> Index(int id)
        {
            var shipment = (await _shipmentService.GetShipmentByIdAsync(id));
            if (shipment == null)
            {
                return Ok("ahmed");
            }
            return View(shipment);
        }

        // GET: Shipment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shipment/Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateDto shipmentDto)
        
        {
            if (ModelState.IsValid)
            {
                await _shipmentService.CreateShipmentAsync(shipmentDto);
                return RedirectToAction("Index");
            }
            return View(shipmentDto);
        }

        // GET: Shipment/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var shipment = await _shipmentService.GetShipmentByIdAsync(id);
        //    if (shipment == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(shipment);
        //}



        public async Task<IActionResult> Delete(int id)
        {
            var shipment = await _shipmentService.DeleteShipmentAsync(id);
            if (shipment)
            {
                return RedirectToAction("Index");

            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _shipmentService.GetShipmentByIdAsync(id);
           
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateDTO shipmentDto)
        {
            if (shipmentDto == null)
            {
                return BadRequest();
            }



            var updatedShipment = await _shipmentService.UpdateShipmentAsync(shipmentDto);

            if (updatedShipment != null)
            {
                return RedirectToAction("Index", "Shipment");
            }
            return NotFound();

           
        }
         
        // POST: Shipment/Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(CreateDto CreateDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _shipmentService.UpdateShipmentAsync(CreateDto);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(CreateDto);
        //}

        //########################################################### 
    }
}
