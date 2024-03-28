using AIMobile.Models.ViewModels;
using System.Transactions;

namespace AIMobileCus.Models.ViewModels
{
    public class NotiViewModel
    {
        public string Id { get; set; }
        //public string ShopProductId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int TotalNetPrice { get; set; }
        public string ScreenShot { get; set; }
        public string PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string? DeliId { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public IList<TransactionViewModel> TransactionList { get; set; }
    }
}
