using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpliftStore.DataAccess.Data.Repository.Interfaces;

namespace UpliftStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
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
                return View(new Models.Category());
            }

            var category = _unitOfWork.CategoryRepository.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Models.Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.CategoryRepository.Add(category);
                }
                else
                {
                    _unitOfWork.CategoryRepository.Update(category);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #region API Calls

        [HttpGet("api/categories")]
        public IActionResult GetAll()
        {
            return new JsonResult(new 
            { 
                data = _unitOfWork.CategoryRepository.GetAll() 
            });
        }

        [HttpDelete("api/[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(id);
            if (category == null)
            {
                return new JsonResult(new 
                { 
                    success = false, 
                    message = "Category was not found" 
                });
            }

            _unitOfWork.CategoryRepository.Remove(id);
            _unitOfWork.Save();

            return new JsonResult(new 
            { 
                success = true, 
                message = "Category deleted successfully" 
            });
        }

        #endregion
    }
}
