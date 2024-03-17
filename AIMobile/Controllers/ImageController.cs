using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageservice;
        public ImageController(IImageService imageService)
        {
            _imageservice = imageService;
        }
        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entry(ImageViewModel ivm)
        {
            try
            {
                string imgUrl = "/Images/";
                var ImageEntity = new ImageEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    ImageName = ivm.ImageName,
                    FrontImageUrl = imgUrl+ ivm.FrontImageUrl,
                    BackImageUrl = imgUrl+ ivm.BackImageUrl,
                    RightSideImageUrl= imgUrl+ ivm.RightSideImageUrl,
                    LeftSideImageUrl= imgUrl+ ivm.LeftSideImageUrl,
                    
                    Filetype = ivm.Filetype,
                    Filesize = ivm.Filesize,

                };
                _imageservice.Entry(ImageEntity);
                TempData["Info"] = "Successfully enter the record to the system";
            }
            catch (Exception e)
            {
                TempData["Info"] = "Error occur when enter the record to the system";

            }
            return View();
        }
        public IActionResult List()
        {
            try
            {
                IList<ImageViewModel> Image = _imageservice.ReteriveAll().Select(s => new ImageViewModel
                {
                    Id = s.Id,
                    ImageName = s.ImageName,
                    FrontImageUrl = s.FrontImageUrl,
                    BackImageUrl = s.BackImageUrl,
                    RightSideImageUrl = s.RightSideImageUrl,
                    LeftSideImageUrl = s.LeftSideImageUrl,
                    Filetype = s.Filetype,
                    Filesize = s.Filesize,
                }).ToList();
                return View(Image);
            }
            catch (Exception)
            {
                TempData["Info"] = "Error occur ";

            }
            return View();
        }
        public IActionResult Delete(string Id)
        {
            try
            {
                _imageservice.Delete(Id);
                TempData["Info"] = "Successfully delete the record to the sytem";
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error cccur when delete the record to the sytem";

            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id)
        {
            try
            {
                var imageDataModel = _imageservice.GetById(Id);
                ImageViewModel ivm = new ImageViewModel();

                if (imageDataModel != null)
                {
                    ivm.Id = imageDataModel.Id;
                    ivm.ImageName = imageDataModel.ImageName;
                    ivm.FrontImageUrl = imageDataModel.FrontImageUrl;
                    ivm.BackImageUrl = imageDataModel.BackImageUrl;
                    ivm.RightSideImageUrl = imageDataModel.RightSideImageUrl;
                    ivm.LeftSideImageUrl = imageDataModel.LeftSideImageUrl;
                    ivm.Filesize = imageDataModel.Filesize;
                    ivm.Filetype = imageDataModel.Filetype;

                };
                return View(ivm);
            }
            catch (Exception)
            {

                TempData["Info"] = "Error cccur";

            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(ImageViewModel ivm)
        {
            try
            {
                string imgUrl = "/Images/";
                ImageEntity imageEntity = new ImageEntity()
                {
                    Id = ivm.Id,
                    ImageName = ivm.ImageName,
                   FrontImageUrl=imgUrl+ivm.FrontImageUrl,
                   BackImageUrl=imgUrl+ivm.BackImageUrl,
                   LeftSideImageUrl=imgUrl+ivm.LeftSideImageUrl,
                   RightSideImageUrl=imgUrl+ivm.RightSideImageUrl,
                    Filesize = ivm.Filesize,
                    Filetype = ivm.Filetype,

                };
                _imageservice.Update(imageEntity);
                TempData["Info"] = "Update Succesfully the recort to the system";
            }
            catch (Exception e)
            {
                TempData["Info"] = "Error occur when update  the recort to the system";
                
            }
            return RedirectToAction("List");
        }
    }
}
