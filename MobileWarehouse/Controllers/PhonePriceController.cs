using Microsoft.AspNetCore.Mvc;
using MobileWarehouse.Entity.Models;
using MobileWarehouse.Entity.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace MobileWarehouse.Controllers
{
    public class PhonePriceController : Controller
    {
        private readonly IPhoneRepository _phoneRepository;

        private readonly IOrderRepository _orderRepository;

        public PhonePriceController(IPhoneRepository phoneRepository, IOrderRepository orderRepository)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<IActionResult> Phone()
        {
            return View(await _phoneRepository.GetPhoneListAsync());
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Phone");
            ViewBag.PhoneId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Buy(Order order)
        {
            _orderRepository.AddOrderAsync(order);
            return RedirectToAction("ThxPage", order);
        }

        public IActionResult ThxPage(Order order)
        {
            ViewBag.Order = order;
            return View();
        }
    }
}
