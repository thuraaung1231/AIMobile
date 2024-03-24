using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMobile.Helper
{
    public class PurchaseReportModel
    {
        public string Id { get; set; }
        public string ShopProductId { get; set; }
        public string CustomerId { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int TotalPrice { get; set; }
        public string ScreenShot { get; set; }
        public string PaymentTypeId { get; set; }
        public string DeliId { get; set; }
        public string TransactionId { get; set; }
    }
}
