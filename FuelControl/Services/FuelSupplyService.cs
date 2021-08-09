using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Text;
using FuelControl.Entities;
using FuelControl.Helpers;
using FuelControl.Models;
using FuelControl.Models.FuelSupplies;
using FuelControl.Dtos.FuelSupplies;
using Microsoft.EntityFrameworkCore;

namespace FuelControl.Services
{
    public interface IFuelSupplyService
    {
        IEnumerable<FuelSupplyResponse> GetAll();
        FuelSupplyResponse GetById(Guid id);
        FuelSupplyResponse Create(CreateFuelSupplyRequest model);
        Task<ServiceResponse<FuelSupplyResponse>> Update(Guid id, UpdateFuelSupplyRequest model);
        void Delete(Guid id);
        Task<ServiceResponse<FuelSupplyResponse>> AddFuelSupply(AddFuelSupplyDtoRequest newFuelSupply);
        Task<ServiceResponse<FuelSupplyFullDetailResponse>> GetFuelSupplyById(Guid id);
    }
    public class FuelSupplyService : IFuelSupplyService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public FuelSupplyService(
            DataContext context,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        public IEnumerable<FuelSupplyResponse> GetAll()
        {
            var fuelSupplies = _context.FuelSupplies;
            return _mapper.Map<IList<FuelSupplyResponse>>(fuelSupplies);
        }

        public FuelSupplyResponse GetById(Guid id)
        {
            var fuelSupply = getFuelSupplies(id);
            return _mapper.Map<FuelSupplyResponse>(fuelSupply);
        }

        public async Task<ServiceResponse<FuelSupplyResponse>> AddFuelSupply(AddFuelSupplyDtoRequest newFuelSupply)
        {
            ServiceResponse<FuelSupplyResponse> response = new ServiceResponse<FuelSupplyResponse>();

            try
            {
                Driver driver = await _context.Drivers.FirstOrDefaultAsync(c => c.Id == newFuelSupply.DriverId);
                if (driver == null)
                {
                    response.Success = false;
                    response.Message = "Driver not found.";
                    return response;
                }

                Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(c => c.Id == newFuelSupply.VehicleId);
                FuelPrice fuelPrice = await _context.FuelPrices.FirstOrDefaultAsync(x => x.Id == newFuelSupply.FuelId);

                if (vehicle == null)
                {
                    response.Success = false;
                    response.Message = "Vehicle not found.";
                    return response;
                }
                if (fuelPrice == null)
                {
                    response.Success = false;
                    response.Message = "Fuel type not found.";
                    return response;
                }
                if (vehicle.MaxTankCapacity < newFuelSupply.TotalLiters)
                {
                    response.Success = false;
                    response.Message = "Vehicle does not support total liters informed.";
                    return response;
                }
                if (vehicle.FuelType != fuelPrice.FuelType)
                {
                    response.Success = false;
                    response.Message = "Vehicle does not support type of fuel informed.";
                    return response;
                }
      

                FuelSupply fuelSupply = new FuelSupply
                {
                    DriverId = newFuelSupply.DriverId,
                    VehicleId = newFuelSupply.VehicleId,
                    FuelId = newFuelSupply.FuelId,
                    TotalLiters = newFuelSupply.TotalLiters,
                    Created = DateTime.UtcNow
                };
                await _context.FuelSupplies.AddAsync(fuelSupply);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<FuelSupplyResponse>(fuelSupply);

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<FuelSupplyFullDetailResponse>> GetFuelSupplyById(Guid id)
        {
            ServiceResponse<FuelSupplyFullDetailResponse> serviceResponse = new ServiceResponse<FuelSupplyFullDetailResponse>();
            FuelSupply dbFuelSupply = await _context.FuelSupplies
                .Include(c => c.Driver)
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(c => c.Id == id);

            FuelSupplyFullDetailResponse fuelSupplyFull = new FuelSupplyFullDetailResponse();

            fuelSupplyFull.Id = dbFuelSupply.Id;
            fuelSupplyFull.DriverId = dbFuelSupply.DriverId;
            fuelSupplyFull.VehicleId = dbFuelSupply.VehicleId;
            fuelSupplyFull.FuelId = dbFuelSupply.FuelId;
            fuelSupplyFull.TotalLiters = dbFuelSupply.TotalLiters;
            fuelSupplyFull.Name = dbFuelSupply.Driver.Name;
            fuelSupplyFull.Cpf = dbFuelSupply.Driver.Cpf;
            fuelSupplyFull.Status = dbFuelSupply.Driver.Status;
            fuelSupplyFull.LicensePlate = dbFuelSupply.Vehicle.LicensePlate;
            fuelSupplyFull.VehicleModel = dbFuelSupply.Vehicle.VehicleModel;
            fuelSupplyFull.FuelType = dbFuelSupply.Vehicle.FuelType;
            fuelSupplyFull.Created = dbFuelSupply.Created;
            fuelSupplyFull.Updated = dbFuelSupply.Updated;

            serviceResponse.Data = _mapper.Map<FuelSupplyFullDetailResponse>(fuelSupplyFull);
            return serviceResponse;
        }
        public FuelSupplyResponse Create(CreateFuelSupplyRequest model)
        {

            var fuelSupply = _mapper.Map<FuelSupply>(model);
            fuelSupply.Created = DateTime.UtcNow;
            _context.FuelSupplies.Add(fuelSupply);
            _context.SaveChanges();
            return _mapper.Map<FuelSupplyResponse>(fuelSupply);
        }

        public async Task<ServiceResponse<FuelSupplyResponse>> Update(Guid id, UpdateFuelSupplyRequest model)
        {
            ServiceResponse<FuelSupplyResponse> response = new ServiceResponse<FuelSupplyResponse>();

            try
            {
                var fuelSupply = getFuelSupplies(id);
                Driver driver = await _context.Drivers.FirstOrDefaultAsync(c => c.Id == fuelSupply.DriverId);
                if (driver == null)
                {
                    response.Success = false;
                    response.Message = "Driver not found.";
                    return response;
                }

                Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(c => c.Id == fuelSupply.VehicleId);
                FuelPrice fuelPrice = await _context.FuelPrices.FirstOrDefaultAsync(x => x.Id == fuelSupply.FuelId);

                if (vehicle == null)
                {
                    response.Success = false;
                    response.Message = "Vehicle not found.";
                    return response;
                }
                if (fuelPrice == null)
                {
                    response.Success = false;
                    response.Message = "Fuel type not found.";
                    return response;
                }
                if (vehicle.MaxTankCapacity < model.TotalLiters)
                {
                    response.Success = false;
                    response.Message = "Vehicle does not support total liters informed.";
                    return response;
                }
                if (vehicle.FuelType != fuelPrice.FuelType)
                {
                    response.Success = false;
                    response.Message = "Vehicle does not support type of fuel informed.";
                    return response;
                }

                _mapper.Map(model, fuelSupply);
                fuelSupply.Updated = DateTime.UtcNow;
                _context.FuelSupplies.Update(fuelSupply);
                _context.SaveChanges();
                response.Data = _mapper.Map<FuelSupplyResponse>(fuelSupply);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public void Delete(Guid id)
        {
            var fuelSupply = getFuelSupplies(id);
            _context.FuelSupplies.Remove(fuelSupply);
            _context.SaveChanges();
        }

        private FuelSupply getFuelSupplies(Guid id)
        {
            var fuelSupply = _context.FuelSupplies.Find(id);
            if (fuelSupply == null) throw new KeyNotFoundException("Fuel supply not found");
            return fuelSupply;
        }
        
    }

    public class FuelSupplyFullDetailResponse
    {
        public Guid Id { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public int FuelId { get; set; }
        public double TotalLiters { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public bool Status { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleModel { get; set; }
        public string FuelType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
