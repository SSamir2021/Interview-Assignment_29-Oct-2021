using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ejyle_Assignment_WebApp.Models;
using Ejyle_Assignment_WebApp.DataAccess;

namespace Ejyle_Assignment_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ManageShipment()
        {
            ShipmentDataAccess shipmentdb = new ShipmentDataAccess();
            IEnumerable<Shipment> lstShipment = shipmentdb.GetAllShipment();

            if(Convert.ToBoolean(TempData["IsCreateSuccess"]))
            {
                ViewBag.SuccessMessage = "Shipment created successfully";
            }
            else if(Convert.ToBoolean(TempData["IsDeleteSuccess"]))
            {
                ViewBag.SuccessMessage = "Shipment deleted successfully";
            }

            return View(lstShipment);
        }

        public IActionResult GetCreateShipment()
        {
            return View("CreateShipment");
        }

        [HttpPost]
        public IActionResult CreateShipment(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                ShipmentDataAccess shipmentdb = new ShipmentDataAccess();
                int returnNum = shipmentdb.AddShipment(shipment);

                if (returnNum > 0)
                {
                    TempData["IsCreateSuccess"] = true;
                }
                else
                {
                    TempData["IsCreateSuccess"] = false;
                }

                return RedirectToAction("ManageShipment");
            }

            return View(shipment);
        }

        public IActionResult DeleteShipment(int shipmentId)
        {
            ShipmentDataAccess shipmentdb = new ShipmentDataAccess();
            int returnNum = shipmentdb.DeleteShipment(shipmentId);

            if (returnNum > 0)
            {
                TempData["IsDeleteSuccess"] = true;
            }
            else
            {
                TempData["IsDeleteSuccess"] = false;
            }

            return RedirectToAction("ManageShipment");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
