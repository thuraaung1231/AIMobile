
using System;

namespace AIMobile.Helper
{
    
    public class PurchaseReportModel
    {
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public string PaymentTypeId { get; set; }
        public string ShopProductId { get; set; }
        public string CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int TotalPrice { get; set; }
        public string ProductName { get; set; }
        public string ShopName { get; set; }

    }
}
