using System.ComponentModel.DataAnnotations.Schema;

namespace AIMobile.Models.ViewModels
{
    public class ImageViewModel
    {
        public string Id { get; set; }
        public string FrontImageUrl { get; set; }
        public string BackImageUrl { get; set; }
        public string LeftSideImageUrl { get; set; }
        public string RightSideImageUrl { get; set; }
        public string ImageName { get; set; }
        public string Filetype { get; set; }
        public string Filesize { get; set; }
    }
}
