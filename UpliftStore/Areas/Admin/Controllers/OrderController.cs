using Microsoft.AspNetCore.Mvc;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Utility;

namespace UpliftStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index(string status)
        {
            ViewData["Status"] = status;

            return View();
        }

        #region API Calls

        [HttpGet("api/orders")]
        public IActionResult GetAll()
        {
            return new JsonResult(new
            {
                data = _unitOfWork.OrderHeaderRepository.GetAll()
            });
        }

        [HttpGet("api/orders/pending")]
        public IActionResult GetAllPending()
        {
            return new JsonResult(new
            {
                data = _unitOfWork.OrderHeaderRepository.GetAll(filter: oh => oh.Status == SD.SubmittedStatus)
            }); ;
        }

        [HttpGet("api/orders/approved")]
        public IActionResult GetAllApproved()
        {
            return new JsonResult(new
            {
                data = _unitOfWork.OrderHeaderRepository.GetAll(filter: oh => oh.Status == SD.ApprovedStatus)
            });
        }

        #endregion
    }
}
