using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Brand")]
    public class BrandEntity:BaseEntity
    {
        public string Name { get; set; }
    }
}
