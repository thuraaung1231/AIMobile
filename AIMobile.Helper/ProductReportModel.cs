using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMobile.Helper
{
    public class ProductReportModel
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }

        public string BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
