using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Text;
using FuelControl.Entities;
using FuelControl.Helpers;
using FuelControl.Models.FuelPrices;

namespace FuelControl.Services
{
    public interface IFuelPriceService
    {
        IEnumerable<FuelPriceResponse> GetAll();
        FuelPriceResponse GetById(int id);
        FuelPriceResponse Create(CreateFuelPriceRequest model);
        FuelPriceResponse Update(int id, UpdateFuelPriceRequest model);
        void Delete(int id);
    }
    public class FuelPriceService : IFuelPriceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public FuelPriceService(
            DataContext context,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<FuelPriceResponse> GetAll()
        {
            var fuelPrices = _context.FuelPrices;
            return _mapper.Map<IList<FuelPriceResponse>>(fuelPrices);
        }

        public FuelPriceResponse GetById(int id)
        {
            var fuelPrices = getFuelPrices(id);
            return _mapper.Map<FuelPriceResponse>(fuelPrices);
        }

        public FuelPriceResponse Create(CreateFuelPriceRequest model)
        {
            if (_context.FuelPrices.Any(x => x.FuelType == model.FuelType))
                throw new AppException($"Fuel '{model.FuelType}' is already registered");

            var fuelPrice = _mapper.Map<FuelPrice>(model);
            fuelPrice.Created = DateTime.UtcNow;
            _context.FuelPrices.Add(fuelPrice);
            _context.SaveChanges();
            return _mapper.Map<FuelPriceResponse>(fuelPrice);
        }

        public FuelPriceResponse Update(int id, UpdateFuelPriceRequest model)
        {
            var fuelPrice = getFuelPrices(id);

            if (fuelPrice.FuelType != model.FuelType && _context.FuelPrices.Any(x => x.FuelType == model.FuelType))
                throw new AppException($"Fuel '{model.FuelType}' is already taken");

            _mapper.Map(model, fuelPrice);
            fuelPrice.Updated = DateTime.UtcNow;
            _context.FuelPrices.Update(fuelPrice);
            _context.SaveChanges();
            return _mapper.Map<FuelPriceResponse>(fuelPrice);
        }

        public void Delete(int id)
        {
            var fuelPrice = getFuelPrices(id);
            _context.FuelPrices.Remove(fuelPrice);
            _context.SaveChanges();
        }

        private FuelPrice getFuelPrices(int id)
        {
            var fuelPrice = _context.FuelPrices.Find(id);
            if (fuelPrice == null) throw new KeyNotFoundException("Fuel not found");
            return fuelPrice;
        }

    }
}
