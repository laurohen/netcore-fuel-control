using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FuelControl.Entities;
using FuelControl.Models.FuelSupplies;
using FuelControl.Dtos.FuelSupplies;
using FuelControl.Services;

namespace FuelControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelSupplyController : ControllerBase
    {
        private readonly IFuelSupplyService _fuelSupplyService;

        public FuelSupplyController(
            IFuelSupplyService fuelSupplyService)
        {
            _fuelSupplyService = fuelSupplyService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFuelSupply(AddFuelSupplyDtoRequest newFuelSupply)
        {
            return Ok(await _fuelSupplyService.AddFuelSupply(newFuelSupply));
        }

        [Authorize]
        [HttpGet("detail/{id:guid}")]
        public async Task<IActionResult> GetFuelSupplyDetail(Guid id)
        {
            return Ok(await _fuelSupplyService.GetFuelSupplyById(id));
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<FuelSupplyResponse>> GetAll()
        {
            var fuelSupplies = _fuelSupplyService.GetAll();
            return Ok(fuelSupplies);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public ActionResult<FuelSupplyResponse> GetById(Guid id)
        {
            var fuelSupply = _fuelSupplyService.GetById(id);
            return Ok(fuelSupply);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateFuelSupplyRequest model)
        {
            var fuelSupply = await _fuelSupplyService.Update(id, model);
            return Ok(fuelSupply);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _fuelSupplyService.Delete(id);
            return Ok(new { message = "Fuel supply deleted successfully" });
        }

    }
}
