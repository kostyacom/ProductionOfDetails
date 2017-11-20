using System;
using System.Collections.Generic;

namespace ProductionOfDetails
{
    public partial class Invoices
    {
        public int IdInvoice { get; set; }
        public int? IdSupplier { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdMaterial { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double? Cost { get; set; }
        public double? Weight { get; set; }

        public Employees IdEmployeeNavigation { get; set; }
        public MaterialTypes IdMaterialNavigation { get; set; }
        public Suppliers IdSupplierNavigation { get; set; }
    }
}
