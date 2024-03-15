using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMobile.Helper
{
    public class ShopProductReportModel
    {
        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageId { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
    }
}
