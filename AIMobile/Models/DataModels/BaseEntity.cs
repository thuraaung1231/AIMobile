using AIMobile.Services.Utilities;
using System.ComponentModel.DataAnnotations;

namespace AIMobile.Models.DataModels
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public string IpAddress { get; set; }=NetworkHelper.GetLocalIpAddress();
    }
}
