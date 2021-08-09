using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FuelControl.Entities;
using FuelControl.Models.FuelPrices;
using FuelControl.Services;

namespace FuelControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelPricesController : ControllerBase
    {
        private readonly IFuelPriceService _fuelPriceService;

        public FuelPricesController(
            IFuelPriceService fuelPriceService)
        {
            _fuelPriceService = fuelPriceService;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<FuelPriceResponse>> GetAll()
        {
            var fuelPrices = _fuelPriceService.GetAll();
            return Ok(fuelPrices);
        }

        [Authorize(Role.Admin)]
        [HttpGet("{id:int}")]
        public ActionResult<FuelPriceResponse> GetById(int id)
        {
            var fuelPrice = _fuelPriceService.GetById(id);
            return Ok(fuelPrice);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public ActionResult<FuelPriceResponse> Create(CreateFuelPriceRequest model)
        {
            var fuelPrice = _fuelPriceService.Create(model);
            return Ok(fuelPrice);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id:int}")]
        public ActionResult<FuelPriceResponse> Update(int id, UpdateFuelPriceRequest model)
        {
            var fuelPrice = _fuelPriceService.Update(id, model);
            return Ok(fuelPrice);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _fuelPriceService.Delete(id);
            return Ok(new { message = "Fuel deleted successfully" });
        }
    }
}
