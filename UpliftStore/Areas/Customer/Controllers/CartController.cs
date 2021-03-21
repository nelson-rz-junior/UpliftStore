using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Extensions;
using UpliftStore.Models;
using UpliftStore.Models.ViewModels;
using UpliftStore.Utility;

namespace UpliftStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CartViewModel CartViewModel { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            CartViewModel = new CartViewModel
            {
                OrderHeader = new OrderHeader(),
                Services = new List<Service>()
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cartItemIds = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            if (cartItemIds != null)
            {
                foreach (var serviceId in cartItemIds)
                {
                    CartViewModel.Services.Add(_unitOfWork.ServiceRepository.GetFirstOrDefault(s => s.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }

            return View(CartViewModel);
        }

        [HttpGet]
        public IActionResult Summary()
        {
            var cartItemIds = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            if (cartItemIds != null)
            {
                foreach (var serviceId in cartItemIds)
                {
                    CartViewModel.Services.Add(_unitOfWork.ServiceRepository.GetFirstOrDefault(s => s.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }

            return View(CartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult PostSummary()
        {
            var cartItemIds = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            if (cartItemIds == null)
            {
                return BadRequest("Empty shopping cart");
            }

            CartViewModel.Services = new List<Service>();

            foreach (var serviceId in cartItemIds)
            {
                CartViewModel.Services.Add(_unitOfWork.ServiceRepository.Get(serviceId));
            }

            if (!ModelState.IsValid)
            {
                return View(CartViewModel);
            }

            CartViewModel.OrderHeader.OrderCreate = DateTime.Now;
            CartViewModel.OrderHeader.Status = SD.SubmittedStatus;
            CartViewModel.OrderHeader.QuantityServices = cartItemIds.Count;

            _unitOfWork.OrderHeaderRepository.Add(CartViewModel.OrderHeader);
            _unitOfWork.Save();

            foreach (var item in CartViewModel.Services)
            {
                OrderDetail orderDetail = new()
                {
                    ServiceId = item.Id,
                    OrderHeaderId = CartViewModel.OrderHeader.Id,
                    ServiceName = item.Name,
                    Price = item.Price
                };

                _unitOfWork.OrderDetailRepository.Add(orderDetail);
            }

            _unitOfWork.Save();

            HttpContext.Session.SetObject(SD.SessionCart, new List<int>());

            return RedirectToAction(nameof(OrderConfirmation), "Cart", new { id = CartViewModel.OrderHeader.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int serviceId)
        {
            var cartItemIds = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            if (cartItemIds != null)
            {
                cartItemIds.Remove(serviceId);
                HttpContext.Session.SetObject(SD.SessionCart, cartItemIds);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
