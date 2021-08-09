using System;
using System.ComponentModel.DataAnnotations;
using FuelControl.Entities;

namespace FuelControl.Models.Drivers
{
    public class UpdateDriverRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Cnh { get; set; }
        public string CnhCategory { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Status { get; set; }
    }
}