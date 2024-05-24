using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROGP2.Models;

namespace PROGP2.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class EmployeeController : Controller
    {
        private readonly AgriEnergyConnectContext _context;

        public EmployeeController(AgriEnergyConnectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFarmer(User user)
        {
            if (ModelState.IsValid)
            {
                user.RoleId = 2;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Additional employee actions here
    }

}
