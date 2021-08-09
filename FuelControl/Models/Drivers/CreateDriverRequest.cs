using System;
using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Models.Drivers
{
    public class CreateDriverRequest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(11)]
        public string Cpf { get; set; }

        [Required]
        [MinLength(5)]
        public string Cnh { get; set; }

        [Required]
        [MinLength(1)]
        public string CnhCategory { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}