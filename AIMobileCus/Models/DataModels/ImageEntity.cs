using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.DataModels
{
    [Table("Tbl_Image")]
    public class ImageEntity:BaseEntity
    {
        public string FrontImageUrl { get; set; }
        public string BackImageUrl { get; set; }
        public string LeftSideImageUrl { get; set; }
        public string RightSideImageUrl { get; set; }
        public string ImageName { get; set; }
        public string Filetype { get; set; }
        public string Filesize { get; set; }
    }
}
