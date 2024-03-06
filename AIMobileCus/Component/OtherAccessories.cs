using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobileCus.Controllers
{
    public class OtherAccessories : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ITypeServices _typeServices;

        public OtherAccessories(IProductService productService, ITypeServices typeServices)
        {
            _productService = productService;
           _typeServices = typeServices;
        }
        public IViewComponentResult Invoke()
        {
          List<TypeViewModel> product = _typeServices.ReteriveAll().Select(s=>new TypeViewModel {Id = s.Id, Name=s.Name }).ToList();
        return View(product);
        }
      
    }
}
