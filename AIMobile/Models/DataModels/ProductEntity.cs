using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Product")]
    public class ProductEntity:BaseEntity
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string TypeId { get; set; }
        public string BrandId { get; set; }
    }
}
