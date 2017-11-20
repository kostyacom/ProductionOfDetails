using System;
using System.Collections.Generic;

namespace ProductionOfDetails
{
    public partial class Orders
    {
        public Orders()
        {
            Details = new HashSet<Details>();
        }

        public int IdOrder { get; set; }
        public int? IdClient { get; set; }
        public int? IdEmployee { get; set; }
        public int? DetailsAmount { get; set; }
        public double? OrderCost { get; set; }
        public double? Discount { get; set; }
        public int? AmountOrdersOnDate { get; set; }
        public int? Chek { get; set; }

        public Clients IdClientNavigation { get; set; }
        public Employees IdEmployeeNavigation { get; set; }
        public ICollection<Details> Details { get; set; }
    }
}
