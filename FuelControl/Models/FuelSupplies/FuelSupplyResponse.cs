using System;

namespace FuelControl.Models.FuelSupplies
{
    public class FuelSupplyResponse
    {
        public Guid Id { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public int FuelId { get; set; }
        public double TotalLiters { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}