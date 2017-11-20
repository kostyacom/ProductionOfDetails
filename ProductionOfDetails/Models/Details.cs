using System;
using System.Collections.Generic;

namespace ProductionOfDetails
{
    public partial class Details
    {
        public int IdDetail { get; set; }
        public int? IdMaterial { get; set; }
        public double? Weight { get; set; }
        public string Colour { get; set; }
        public double? Diameter { get; set; }
        public double? Cost { get; set; }
        public int? IdOrder { get; set; }
        public int? AmountDetails { get; set; }

        public MaterialTypes IdMaterialNavigation { get; set; }
        public Orders IdOrderNavigation { get; set; }
    }
}
