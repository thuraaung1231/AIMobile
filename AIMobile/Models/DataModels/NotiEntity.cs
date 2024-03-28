using AIMobile.Models.DataModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobileCus.Models.DataModels
{
    [Table("tbl_Noti")]
    public class NotiEntity:BaseEntity
    {
        public string ShopProductId { get; set; }
        public string CustomerId { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int TotalPrice { get; set; }
        public string ScreenShot { get; set; }
        public string PaymentTypeId { get; set; }
        public string? DeliId { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public string CustomerEmail { get; set; }
        public int NumberOfItem { get; set; }
    }

}
