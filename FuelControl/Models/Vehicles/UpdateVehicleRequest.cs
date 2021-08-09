using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Models.Vehicles
{
    public class UpdateVehicleRequest
    {
        public string LicensePlate { get; set; }
        public string VehicleModel { get; set; }
        public string FuelType { get; set; }
        public string Manufacturer { get; set; }
        public string YearManufacture { get; set; }
        public string MaxTankCapacity { get; set; }
        public string Comments { get; set; }
    }
}