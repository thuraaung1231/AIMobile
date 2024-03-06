using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobileCus.Controllers
{
    public class CatagoryMenu:ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ITypeServices _typeServices;

        public CatagoryMenu(IProductService productService, ITypeServices typeServices)
        {
            _productService = productService;
           _typeServices = typeServices;
        }
        public IViewComponentResult Invoke()
        {
          List<ProductViewModel> product = _productService.ReteriveAll().Select(s=>new ProductViewModel{Id=s.Id, Name=s.Name }).ToList();
        return View(product);
        }
      
    }
}
