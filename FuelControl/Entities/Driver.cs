using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelControl.Entities
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Cnh { get; set; }
        public string CnhCategory { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public ICollection<FuelSupply> FuelSupplies { get; set; }

    }
}
