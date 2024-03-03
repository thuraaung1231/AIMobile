using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Shop")]
    public class ShopEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
    }
}
