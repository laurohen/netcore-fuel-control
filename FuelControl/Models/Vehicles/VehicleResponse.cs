using System;

namespace FuelControl.Models.Vehicles
{
    public class VehicleResponse
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleModel { get; set; }
        public string FuelType { get; set; }
        public string Manufacturer { get; set; }
        public string YearManufacture { get; set; }
        public string MaxTankCapacity { get; set; }
        public string Comments { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}