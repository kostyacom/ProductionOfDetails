using System;
using System.Collections.Generic;

namespace ProductionOfDetails
{
    public partial class Clients
    {
        public Clients()
        {
            Orders = new HashSet<Orders>();
        }

        public int IdClient { get; set; }
        public double? Discount { get; set; }
        public string Telephone { get; set; }
        public string Adress { get; set; }
        public string FirmName { get; set; }
        public string Representative { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
