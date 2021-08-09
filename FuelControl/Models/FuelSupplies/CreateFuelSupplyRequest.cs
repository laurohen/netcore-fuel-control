using System;
using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Models.FuelSupplies
{
    public class CreateFuelSupplyRequest
    {
        [Required]
        public Guid DriverId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public int FuelId { get; set; }

        [Required]
        [MinLength(1)]
        public float TotalLiters { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}