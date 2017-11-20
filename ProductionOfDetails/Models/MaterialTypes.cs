using System;
using System.Collections.Generic;

namespace ProductionOfDetails
{
    public partial class MaterialTypes
    {
        public MaterialTypes()
        {
            Details = new HashSet<Details>();
            Invoices = new HashSet<Invoices>();
        }

        public int IdMaterial { get; set; }
        public string TypeMaterial { get; set; }

        public ICollection<Details> Details { get; set; }
        public ICollection<Invoices> Invoices { get; set; }
    }
}
