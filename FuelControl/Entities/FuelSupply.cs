using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelControl.Entities
{
    public class FuelSupply
    {
        public Guid Id { get; set; }
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int FuelId { get; set; }
        public double TotalLiters { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
