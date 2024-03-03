using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Shop_Product")]
    public class ShopProductEntity:BaseEntity
    {

        public string Shop_Id { get; set; }
        public string Product_Id { get; set; }
        public string Image_Id { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
    }
}
