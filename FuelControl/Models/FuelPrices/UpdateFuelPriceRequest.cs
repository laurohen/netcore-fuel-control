using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Models.FuelPrices
{
    public class UpdateFuelPriceRequest
    {
        public string FuelType { get; set; }
        public decimal Price { get; set; }
    }
}