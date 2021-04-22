using Microsoft.AspNetCore.Mvc;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Models.ViewModels;
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            OrderViewModel orderViewModel = new OrderViewModel
            {
                OrderHeader = _unitOfWork.OrderHeaderRepository.Get(id),
                OrderDetails = _unitOfWork.OrderDetailRepository.GetAll(filter: d => d.OrderHeaderId == id)
            };

            return View(orderViewModel);
        }

        [HttpGet]
        public IActionResult Approve(int id)
        {
            var orderHeader = _unitOfWork.OrderHeaderRepository.Get(id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            _unitOfWork.OrderHeaderRepository.ChangeOrderStatus(id, SD.ApprovedStatus);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public IActionResult Reject(int id)
        {
            var orderHeader = _unitOfWork.OrderHeaderRepository.Get(id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            _unitOfWork.OrderHeaderRepository.ChangeOrderStatus(id, SD.RejectedStatus);

            return RedirectToAction(nameof(Details), new { id });
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
