using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Extensions;
using UpliftStore.Models;
using UpliftStore.Models.ViewModels;
using UpliftStore.Utility;

namespace UpliftStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Services = _unitOfWork.ServiceRepository.GetAll(includeProperties: "Frequency"),
                Categories = _unitOfWork.CategoryRepository.GetAll(orderBy: q => q.OrderBy(c => c.DisplayOrder))
            };

            return View(homeViewModel);
        }

        public IActionResult Details(int id)
        {
            var service = _unitOfWork.ServiceRepository.GetFirstOrDefault(includeProperties: "Category,Frequency", filter: s => s.Id == id);
            return View(service);
        }

        public IActionResult AddToCart(int serviceId)
        {
            List<int> cartItemIds = new List<int>();
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SD.SessionCart)))
            {
                cartItemIds.Add(serviceId);
                HttpContext.Session.SetObject(SD.SessionCart, cartItemIds);
            }
            else
            {
                cartItemIds = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                if (!cartItemIds.Contains(serviceId))
                {
                    cartItemIds.Add(serviceId);
                    HttpContext.Session.SetObject(SD.SessionCart, cartItemIds);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
