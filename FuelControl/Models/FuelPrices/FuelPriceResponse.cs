using System;

namespace FuelControl.Models.FuelPrices
{
    public class FuelPriceResponse
    {
        public int Id { get; set; }
        public string FuelType { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}