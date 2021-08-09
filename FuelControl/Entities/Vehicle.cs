using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelControl.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleModel { get; set; }
        public string FuelType { get; set; }
        public string Manufacturer { get; set; }
        public string YearManufacture { get; set; }
        public double MaxTankCapacity { get; set; }
        public string Comments { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public ICollection<FuelSupply> FuelSupplies { get; set; }
    }
}
