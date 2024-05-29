using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notification;
using PROGP2.Models;
using System.Net.Http;

namespace PROGP2.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class EmployeeController : Controller
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();

        HttpClient httpClient = new HttpClient();

        public async  Task<IActionResult> Index()
        {
            
            var farmers = await context.Users.Where(u => u.Role.RoleName == "Farmer").ToListAsync();
            ViewBag.Farmers = new SelectList(farmers, "UserId", "Username");

            
            var categories = await context.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

            
            var products = await context.Products
                .Include(p => p.Category)
                .Include(p => p.User)
                .ToListAsync();

           
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:7242/api/Notifications");

           
            IEnumerable<NotificationData> notifications = new List<NotificationData>();


            if (response.IsSuccessStatusCode)
            {
                notifications = await response.Content.ReadAsAsync<IEnumerable<NotificationData>>();
            }

            
            var viewModel = new IndexViewModel
            {
                Products = products,
                Notifications = notifications
            };

            return View(viewModel);
        }
        [HttpPost]

        public async Task<IActionResult> Index(int farmerId, DateOnly? startDate, DateOnly? endDate, int? categoryId)
        {
            var list = context.Products
                .Include(p => p.Category)
                .Include(p => p.User)
                .AsQueryable();

            if (farmerId > 0)
            {
                list = list.Where(p => p.UserId == farmerId);
            }

            if (startDate !=null || endDate != null)
            {
               list = list.Where(p => p.ProductionDate >= startDate && p.ProductionDate <= endDate);
            }

            if (categoryId != null)
            {
                list = list.Where(p => p.CategoryId == categoryId);
            }

            var products = await list.ToListAsync();

            var farmers = await context.Users.Where(u => u.Role.RoleName == "Farmer").ToListAsync();
            ViewBag.Farmers = new SelectList(farmers, "UserId", "Username");

            var categories = await context.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

            return View(products);
        }
        public IActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFarmer(User user)
        {
            
                user.RoleId = 2;
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            
           
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
        public async Task<IActionResult> FilterByDate(DateOnly startDate, DateOnly endDate)
        {
            var filteredProducts = await context.Products
                .Where(p => p.ProductionDate >= startDate && p.ProductionDate <= endDate)
                .Include(p => p.Category)
                .Include(p => p.User)
                .ToListAsync();

            return View("Filtered", filteredProducts);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveNotification(int notificationId)
        {
            var response = httpClient.DeleteAsync($"https://localhost:7242/api/Notifications{notificationId}").Result;
            {
                //var response = await httpClient.DeleteAsync($"api/Notifications/{notificationId}");

                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }
    }

}
