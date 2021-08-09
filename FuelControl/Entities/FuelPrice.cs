using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelControl.Entities
{
    public class FuelPrice
    {
        public int Id { get; set; }
        public string FuelType { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
