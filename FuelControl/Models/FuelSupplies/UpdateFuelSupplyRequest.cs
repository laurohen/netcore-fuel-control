using System;
using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Models.FuelSupplies
{
    public class UpdateFuelSupplyRequest
    {
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public int FuelId { get; set; }
        public float TotalLiters { get; set; }
    }
}