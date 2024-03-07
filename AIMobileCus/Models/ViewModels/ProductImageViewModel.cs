using AIMobile.Models.ViewModels;

namespace AIMobileCus.Models.ViewModels
{
    public class ProductImageViewModel
    {
        public IList<ProductViewModel> Products { get; set; }
        public IList<ImageViewModel> Images { get; set; }
    }
}
