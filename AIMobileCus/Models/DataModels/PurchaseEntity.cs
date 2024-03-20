using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Purchase")]
    public class PurchaseEntity : BaseEntity
    {
        public string Id { get; set; }
        public string ShopProduct_Id { get; set; }
        public string Customer_Id { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int TotalPrice { get; set; }
        public string ScreenShot { get; set; }
        public string PaymentType_Id { get; set; }
        public string? Deli_Id { get; set; }
    }
}
