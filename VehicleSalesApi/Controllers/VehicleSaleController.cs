using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleSales.Model.Interfaces.Engine.VehicleSales;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleSalesApi.Controllers
{
    [Route("api/vehicleSales")]
    [ApiController]
    public class VehicleSaleController : ControllerBase
    {
        private readonly IVehicleSalesEngine _vehicleSalesEngine;

        public VehicleSaleController(IVehicleSalesEngine vehicleSalesEngine)
        {
            _vehicleSalesEngine = vehicleSalesEngine;
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            var response = _vehicleSalesEngine.GetVehicleSales();
            return Ok(response);
        }
    }
}
