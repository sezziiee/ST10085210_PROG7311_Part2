using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROGP2.Models;
using System.Security.Claims;

namespace PROGP2.Controllers
{
    [Authorize(Policy = "FarmerOnly")]
    public class FarmerController : Controller
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var products = await context.Products
                .Include(p => p.Category)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return View("Index", products);

        }

        public IActionResult AddProduct()
        {
            var categories = context.Categories.ToList();
            ViewBag.Category = getCategories();
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Farmer")]
        public IActionResult AddProduct(string name, string description, string categoryid, DateOnly productiondate)
        {
            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine($"{UserId}\n{name}\n{description}\n{categoryid}\n{productiondate}");
            if (ModelState.IsValid)
            {
                
                context.Products.Add(new Product()
                {
                    UserId = UserId,
                    Name = name,
                    Description = description,
                    CategoryId = int.Parse(categoryid),
                    ProductionDate = productiondate
                });
                
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

           
        }
        private IEnumerable<SelectListItem> getCategories()
        {
            
            // Fetch categories from the database and map to SelectListItem
            return context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList();
            
        }
    }

    }
    

