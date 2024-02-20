using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Type")]
    public class TypeEntity:BaseEntity
    {
        public string Name { get; set; }
    }
}
