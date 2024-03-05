using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Shop_Product")]
    public class ShopProductEntity:BaseEntity
    {
        public string ShopId { get; set; }
        public string ProductId { get; set; }
        public string ImageId { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
    }
}
