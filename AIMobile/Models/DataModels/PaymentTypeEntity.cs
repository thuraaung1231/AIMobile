using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("tbl_paymenttype")]
    public class PaymentTypeEntity : BaseEntity 

    {
        public string PaymentType { get; set; }
    }
}
