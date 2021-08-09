using System;
using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Dtos.FuelSupplies
{
    public class AddFuelSupplyDtoRequest
    {
        [Required]
        public Guid DriverId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public int FuelId { get; set; }

        [Required]
        public double TotalLiters { get; set; }

    }
}