using System;
using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Models.FuelPrices
{
    public class CreateFuelPriceRequest
    {
        [Required]
        [MinLength(3)]
        public string FuelType { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}