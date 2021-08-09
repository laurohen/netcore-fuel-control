using System;
using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Models.Vehicles
{
    public class CreateVehicleRequest
    {
        [Required]
        [MinLength(5)]
        public string LicensePlate { get; set; }

        [Required]
        [MinLength(3)]
        public string VehicleModel { get; set; }

        [Required]
        [MinLength(3)]
        public string FuelType { get; set; }

        [Required]
        [MinLength(3)]
        public string Manufacturer { get; set; }

        [Required]
        public string YearManufacture { get; set; }
        
        [Required]
        public string MaxTankCapacity { get; set; }

        public string Comments { get; set; }
    }
}