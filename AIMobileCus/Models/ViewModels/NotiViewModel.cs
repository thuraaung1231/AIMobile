namespace AIMobileCus.Models.ViewModels
{
    public class NotiViewModel
    {
        public string Id { get; set; }
        public string ShopProductId { get; set; }
        public string CustomerId { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int TotalPrice { get; set; }
        public string ScreenShot { get; set; }
        public string PaymentTypeId { get; set; }
        public string? DeliId { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int NumberOfItem { get; set; }
    }
}
