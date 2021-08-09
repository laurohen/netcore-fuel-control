using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Text;
using FuelControl.Entities;
using FuelControl.Helpers;
using FuelControl.Models.Drivers;

namespace FuelControl.Services
{
    public interface IDriverService
    {
        IEnumerable<DriverResponse> GetAll();
        DriverResponse GetById(Guid id);
        DriverResponse Create(CreateDriverRequest model);
        DriverResponse Update(Guid id, UpdateDriverRequest model);
        void Delete(Guid id);
    }
    public class DriverService : IDriverService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public DriverService(
            DataContext context,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<DriverResponse> GetAll()
        {
            var drivers = _context.Drivers;
            return _mapper.Map<IList<DriverResponse>>(drivers);
        }

        public DriverResponse GetById(Guid id)
        {
            var driver = getDriver(id);
            return _mapper.Map<DriverResponse>(driver);
        }

        public DriverResponse Create(CreateDriverRequest model)
        {
            if (_context.Drivers.Any(x => x.Cpf == model.Cpf))
                throw new AppException($"Cpf '{model.Cpf}' is already registered");

            var driver = _mapper.Map<Driver>(model);
            driver.Created = DateTime.UtcNow;
            _context.Drivers.Add(driver);
            _context.SaveChanges();
            return _mapper.Map<DriverResponse>(driver);
        }

        public DriverResponse Update(Guid id, UpdateDriverRequest model)
        {
            var driver = getDriver(id);

            if (driver.Cpf != model.Cpf && _context.Drivers.Any(x => x.Cpf == model.Cpf))
                throw new AppException($"Cpf '{model.Cpf}' is already taken");

            _mapper.Map(model, driver);
            driver.Updated = DateTime.UtcNow;
            _context.Drivers.Update(driver);
            _context.SaveChanges();
            return _mapper.Map<DriverResponse>(driver);
        }

        public void Delete(Guid id)
        {
            var driver = getDriver(id);
            _context.Drivers.Remove(driver);
            _context.SaveChanges();
        }

        private Driver getDriver(Guid id)
        {
            var driver = _context.Drivers.Find(id);
            if (driver == null) throw new KeyNotFoundException("Driver not found");
            return driver;
        }
    }
}
