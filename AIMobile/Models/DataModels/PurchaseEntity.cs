using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Purchase")]
    public class PurchaseEntity:BaseEntity
    {

        public string  ShopProductId { get; set; }
        public string CustomerId { get; set; }
        public DateTime? PurchaseDateTime { get; set; }
        public int TotalPrice { get; set; }

        public string PaymentTypeId { get; set; }
        public string DeliId { get; set; }
        public string ScreenShot { get; set; }
        public string TransactionId { get; set; }
        public string ShopName { get; set; }
       

        public string ShopProductId { get; set; }
        public string CustomerId { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int TotalPrice { get; set; }
        public string ScreenShot { get; set; }
        public string PaymentTypeId { get; set; }
        public string? DeliId { get; set; }
        public string TransactionId { get; set; }

    }
}
