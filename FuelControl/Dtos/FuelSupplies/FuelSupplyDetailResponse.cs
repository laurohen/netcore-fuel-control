using FuelControl.Entities;
using System;
using System.Collections.Generic;

namespace FuelControl.Dtos.FuelSupplies
{
    public class FuelSupplyDetailResponse
    {
        public Guid Id { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public int FuelId { get; set; }
        public double TotalLiters { get; set; }
        public class Driver
        {
            public string Name { get; set; }
            public string Cpf { get; set; }
            public bool Status { get; set; }
        }

        public class Vehicle
        {
            public string LicensePlate { get; set; }
            public string VehicleModel { get; set; }
            public string FuelType { get; set; }
        }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }

    
}