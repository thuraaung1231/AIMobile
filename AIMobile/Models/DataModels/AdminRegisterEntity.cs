using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Admin")]
    public class AdminRegisterEntity
    {
        [Key]
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
