using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UpliftStore.DataAccess.Data.Repository.Interfaces;

namespace UpliftStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var users = _unitOfWork.UserRepository.GetAll(u => u.Id != claims.Value);

            return View(users);
        }

        [HttpGet]
        public IActionResult Lock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _unitOfWork.UserRepository.LockUser(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Unlock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _unitOfWork.UserRepository.UnlockUser(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
