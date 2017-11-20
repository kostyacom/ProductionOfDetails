using System;
using System.Collections.Generic;

namespace ProductionOfDetails
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Invoices = new HashSet<Invoices>();
        }

        public int IdSupplier { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int? TelephonNumber { get; set; }

        public ICollection<Invoices> Invoices { get; set; }
    }
}
