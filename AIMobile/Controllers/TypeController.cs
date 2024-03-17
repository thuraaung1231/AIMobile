using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class TypeController : Controller
    {
        private readonly ITypeServices _typeServices;

        public TypeController(ITypeServices typeServices)
        {
            _typeServices = typeServices;
        }
        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(TypeViewModel tvm)
        {
            try
            {
                var Types = _typeServices.ReteriveAll().Where(w => w.Name == tvm.Name);
                if (Types.Any())
                {
                    TempData["info"] = "Name already exists in the database";
                }
                else
                {
                    var TypeEntity = new TypeEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = tvm.Name,

                    };
                    _typeServices.Entry(TypeEntity);
                    TempData["Info"] = "Entry Success";
                }
                

            }
            catch (Exception)
            {
                TempData["Info"] = "Entry Unsuccess";

            }
            return View();
        }
        public IActionResult list()
        {
            try
            {
                IList<TypeViewModel> Type = _typeServices.ReteriveAll().Select(b => new TypeViewModel
                {
                    Id = b.Id,
                    Name = b.Name,

                }).ToList();
                return View(Type);
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur";
            }
            return View();
           
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                _typeServices.Delete(Id);
                TempData["Info"] = "Successful Delete ";
            }
            catch (Exception)
            {
                TempData["Info"] = "Unsuccessful Delete ";
                throw;
            }
            return RedirectToAction("List");
        }
        [HttpGet]

        public IActionResult Edit(string Id)
        {
            try
            {
                var typeDataModel = _typeServices.GetById(Id);
                TypeViewModel tvm = new TypeViewModel();
                TypeEntity typeEntity = new TypeEntity();
                if (typeDataModel != null)
                {
                    tvm.Id = typeDataModel.Id;
                    tvm.Name = typeDataModel.Name;
                };

                return View(tvm);
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur";
            }
            return View();
            

        }
        [HttpPost]
        public IActionResult Edit(TypeViewModel tvm)
        {
            try
            {
                TypeEntity typeEntity = new TypeEntity()
                {
                    Id = tvm.Id,
                    Name = tvm.Name,
                };
                _typeServices.Update(typeEntity);
                TempData["Info"] = "Update Successful";
            }
            catch (Exception)
            {
                TempData["Info"] = "Update Unsuccessful";
                throw;
            }
            return RedirectToAction("List");

        }
    }

    }

