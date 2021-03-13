using Microsoft.AspNetCore.Mvc;
using UpliftStore.DataAccess.Data.Repository.Interfaces;

namespace UpliftStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FrequencyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FrequencyController(IUnitOfWork unitOfWork)
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
                return View(new Models.Frequency());
            }

            var frequency = _unitOfWork.FrequencyRepository.Get(id.GetValueOrDefault());
            if (frequency == null)
            {
                return NotFound();
            }

            return View(frequency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Models.Frequency frequency)
        {
            if (ModelState.IsValid)
            {
                if (frequency.Id == 0)
                {
                    _unitOfWork.FrequencyRepository.Add(frequency);
                }
                else
                {
                    _unitOfWork.FrequencyRepository.Update(frequency);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(frequency);
        }

        #region API Calls

        [HttpGet("api/[controller]")]
        public IActionResult Get()
        {
            return new JsonResult(new
            {
                data = _unitOfWork.FrequencyRepository.GetAll()
            });
        }

        [HttpDelete("api/[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            var frequency = _unitOfWork.FrequencyRepository.Get(id);
            if (frequency == null)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Frequency was not found"
                });
            }

            _unitOfWork.FrequencyRepository.Remove(frequency);
            _unitOfWork.Save();

            return new JsonResult(new
            {
                success = true,
                message = "Frequency deleted successfully"
            });
        }

        #endregion
    }
}
