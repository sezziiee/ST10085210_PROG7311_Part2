using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROGP2.Models;

namespace PROGP2.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class EmployeeController : Controller
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();



        public IActionResult Index()
        {
            var products = context.Products.ToList();
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
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<IActionResult> FilterByFarmer(int farmerId)
        {
            var farmerProducts = await context.Products
                .Where(p => p.UserId == farmerId)
                .Include(p => p.Category)
                .Include(p => p.User)
                .ToListAsync();

            return View("FilteredProducts", farmerProducts);
        }
        public async Task<IActionResult> FilterByDate(DateTime startDate, DateTime endDate)
        {
            var productsInRange = await context.Products
                .Where(p => p.ProductionDate >= DateOnly.FromDateTime(startDate) && p.ProductionDate <= DateOnly.FromDateTime(endDate))
                .Include(p => p.Category)
                .Include(p => p.User)
                .ToListAsync();

            return View("FilteredProducts", productsInRange);
        }

    }

}
