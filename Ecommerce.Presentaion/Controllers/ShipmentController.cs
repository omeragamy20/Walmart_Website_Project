﻿using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Shipment;
using System.Threading.Tasks;
using Ecommerce.Application.Services;

namespace Ecommerce.Web.Controllers
{


   
    public class ShipmentController : Controller
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        // GET: Shipment
        public async Task<IActionResult> Index()
        {
            var shipments = await _shipmentService.GetAllShipmentsAsync();
            return View(shipments);
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
        public async Task<IActionResult> Edit(int id)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        // POST: Shipment/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateDto CreateDto)
        {
            if (ModelState.IsValid)
            {
                await _shipmentService.UpdateShipmentAsync(CreateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(CreateDto);
        }

        // GET: Shipment/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        // POST: Shipment/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _shipmentService.DeleteShipmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}