using System;
using System.Collections.Generic;

namespace ProductionOfDetails
{
    public partial class Employees
    {
        public Employees()
        {
            Invoices = new HashSet<Invoices>();
            Orders = new HashSet<Orders>();
        }

        public int IdEmployee { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string FirmName { get; set; }

        public ICollection<Invoices> Invoices { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
