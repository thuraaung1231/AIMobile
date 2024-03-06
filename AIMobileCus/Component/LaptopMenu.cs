using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobileCus.Controllers
{
    public class LaptopMenu:ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ITypeServices _typeServices;

        public LaptopMenu(IProductService productService, ITypeServices typeServices)
        {
            _productService = productService;
           _typeServices = typeServices;
        }
       
        public IViewComponentResult Invoke()
        {
            List<ProductViewModel> product = _productService.ReteriveAll().Where(w=>w.TypeId==_typeServices.GetByName("Laptop").Id).Select(s => new ProductViewModel {Id=s.Id, Name = s.Name }).ToList();
            return View(product);
        }
    }
}
