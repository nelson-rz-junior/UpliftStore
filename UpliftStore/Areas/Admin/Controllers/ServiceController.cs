using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Models.ViewModels;

namespace UpliftStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public IFormFileCollection UploadImage { get; set; }

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ServiceViewModel serviceViewModel = new()
            {
                Service = new Models.Service(),
                Categories = _unitOfWork.CategoryRepository.GetCategoryListForDropDown(),
                Frequencies = _unitOfWork.FrequencyRepository.GetFrequencyListForDropDown()
            };

            if (id.HasValue)
            {
                serviceViewModel.Service = _unitOfWork.ServiceRepository.Get(id.GetValueOrDefault());
            }

            return View(serviceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ServiceViewModel serviceViewModel)
        {
            if (ModelState.IsValid)
            {
                string webrootPath = _hostEnvironment.WebRootPath;

                if (serviceViewModel.Service.Id == 0)
                {
                    var fileName = UploadFile(webrootPath, UploadImage);
                    serviceViewModel.Service.ImageFile = fileName;

                    _unitOfWork.ServiceRepository.Add(serviceViewModel.Service);
                }
                else
                {
                    var service = _unitOfWork.ServiceRepository.Get(serviceViewModel.Service.Id);
                    if (UploadImage.Count > 0)
                    {
                        string imagePath = Path.Combine(webrootPath, "images", "services", service.ImageFile);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        var fileName = UploadFile(webrootPath, UploadImage);

                        serviceViewModel.Service.ImageFile = fileName;
                    }
                    else
                    {
                        serviceViewModel.Service.ImageFile = service.ImageFile;
                    }

                    _unitOfWork.ServiceRepository.Update(serviceViewModel.Service);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(serviceViewModel);
        }

        #region API Calls

        [HttpGet("api/services")]
        public IActionResult GetAll()
        {
            return new JsonResult(new
            {
                data = _unitOfWork.ServiceRepository.GetAll(includeProperties: "Category,Frequency")
            });
        }

        [HttpDelete("api/[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var service = _unitOfWork.ServiceRepository.GetFirstOrDefault(s => s.Id == id);
                if (service == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Error while deleting"
                    });
                }

                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "services", service.ImageFile);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _unitOfWork.ServiceRepository.Remove(service);
                _unitOfWork.Save();

                return new JsonResult(new
                {
                    success = true,
                    message = "Delete successful"
                });
            }
            catch (Exception)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "An exception occurred while deleting"
                });
            }
        }

        #endregion

        #region Private Methods

        private string UploadFile(string webrootPath, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(webrootPath, "images", "services");
            string extension = Path.GetExtension(files[0].FileName);
            string fileName = Guid.NewGuid() + extension;

            using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            return fileName;
        }

        #endregion
    }
}
