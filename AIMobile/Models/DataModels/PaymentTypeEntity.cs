using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_PaymentType")]
    public class PaymentTypeEntity : BaseEntity 

    {
        public string PaymentType { get; set; }
    }
}
