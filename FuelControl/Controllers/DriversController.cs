using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FuelControl.Entities;
using FuelControl.Models.Drivers;
using FuelControl.Services;

namespace FuelControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly IMapper _mapper;

        public DriversController(
            IDriverService driverService,
            IMapper mapper)
        {
            _driverService = driverService;
            _mapper = mapper;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<DriverResponse>> GetAll()
        {
            var drivers = _driverService.GetAll();
            return Ok(drivers);
        }

        [Authorize(Role.Admin)]
        [HttpGet("{id:int}")]
        public ActionResult<DriverResponse> GetById(Guid id)
        {
            var driver = _driverService.GetById(id);
            return Ok(driver);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public ActionResult<DriverResponse> Create(CreateDriverRequest model)
        {
            var driver = _driverService.Create(model);
            return Ok(driver);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id:guid}")]
        public ActionResult<DriverResponse> Update(Guid id, UpdateDriverRequest model)
        {
            var driver = _driverService.Update(id, model);
            return Ok(driver);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _driverService.Delete(id);
            return Ok(new { message = "Driver deleted successfully" });
        }
    }
}
