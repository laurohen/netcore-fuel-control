using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FuelControl.Entities;
using FuelControl.Models.Vehicles;
using FuelControl.Services;

namespace FuelControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(
            IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<VehicleResponse>> GetAll()
        {
            var vehicles = _vehicleService.GetAll();
            return Ok(vehicles);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public ActionResult<VehicleResponse> GetById(Guid id)
        {
            var vehicle = _vehicleService.GetById(id);
            return Ok(vehicle);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<VehicleResponse> Create(CreateVehicleRequest model)
        {
            var vehicle = _vehicleService.Create(model);
            return Ok(vehicle);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public ActionResult<VehicleResponse> Update(Guid id, UpdateVehicleRequest model)
        {
            var vehicle = _vehicleService.Update(id, model);
            return Ok(vehicle);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _vehicleService.Delete(id);
            return Ok(new { message = "Vehicle deleted successfully" });
        }

    }
}
