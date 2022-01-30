using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using UpliftStore.DataAccess.Data.Repository.Interfaces;

namespace UpliftStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class WebImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WebImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (!id.HasValue)
            {
                return View(new Models.WebImage());
            }

            var webImage = _unitOfWork.WebImageRepository.Get(id.GetValueOrDefault());
            if (webImage == null)
            {
                return NotFound();
            }

            return View(webImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Models.WebImage webImage)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] picture = null;

                    using (var fs = files[0].OpenReadStream())
                    {
                        using var ms = new MemoryStream();

                        fs.CopyTo(ms);
                        picture = ms.ToArray();
                    }

                    webImage.Picture = picture;
                }
                else
                {
                    var currentWebImage = _unitOfWork.WebImageRepository.Get(webImage.Id);
                    webImage.Picture = currentWebImage?.Picture;
                }

                if (webImage.Id == 0)
                {
                    _unitOfWork.WebImageRepository.Add(webImage);
                }
                else
                {

                    _unitOfWork.WebImageRepository.Update(webImage);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(webImage);
        }

        #region API Calls

        [HttpGet("api/webimages")]
        public IActionResult GetAll()
        {
            return new JsonResult(new 
            { 
                data = _unitOfWork.WebImageRepository.GetAll()
            });
        }

        [HttpDelete("api/[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            var webImage = _unitOfWork.WebImageRepository.Get(id);
            if (webImage == null)
            {
                return new JsonResult(new 
                { 
                    success = false, 
                    message = "Image was not found" 
                });
            }

            _unitOfWork.WebImageRepository.Remove(id);
            _unitOfWork.Save();

            return new JsonResult(new 
            { 
                success = true, 
                message = "Image deleted successfully" 
            });
        }

        #endregion
    }
}
