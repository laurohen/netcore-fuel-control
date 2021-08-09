using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Text;
using FuelControl.Entities;
using FuelControl.Helpers;
using FuelControl.Models.Vehicles;

namespace FuelControl.Services
{
    public interface IVehicleService
    {
        IEnumerable<VehicleResponse> GetAll();
        VehicleResponse GetById(Guid id);
        VehicleResponse Create(CreateVehicleRequest model);
        VehicleResponse Update(Guid id, UpdateVehicleRequest model);
        void Delete(Guid id);
    }
    public class VehicleService : IVehicleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public VehicleService(
            DataContext context,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<VehicleResponse> GetAll()
        {
            var vehicles = _context.Vehicles;
            return _mapper.Map<IList<VehicleResponse>>(vehicles);
        }

        public VehicleResponse GetById(Guid id)
        {
            var vehicle = getVehicle(id);
            return _mapper.Map<VehicleResponse>(vehicle);
        }

        public VehicleResponse Create(CreateVehicleRequest model)
        {
            if (_context.Vehicles.Any(x => x.LicensePlate == model.LicensePlate))
                throw new AppException($"License plate '{model.LicensePlate}' is already registered");

            var vehicle = _mapper.Map<Vehicle>(model);
            vehicle.Created = DateTime.UtcNow;
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
            return _mapper.Map<VehicleResponse>(vehicle);
        }

        public VehicleResponse Update(Guid id, UpdateVehicleRequest model)
        {
            var vehicle = getVehicle(id);

            if (vehicle.LicensePlate != model.LicensePlate && _context.Vehicles.Any(x => x.LicensePlate == model.LicensePlate))
                throw new AppException($"License plate : '{model.LicensePlate}' is already taken");

            _mapper.Map(model, vehicle);
            vehicle.Updated = DateTime.UtcNow;
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
            return _mapper.Map<VehicleResponse>(vehicle);
        }

        public void Delete(Guid id)
        {
            var vehicle = getVehicle(id);
            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
        }

        private Vehicle getVehicle(Guid id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null) throw new KeyNotFoundException("Vehicle not found");
            return vehicle;
        }
    }
}
