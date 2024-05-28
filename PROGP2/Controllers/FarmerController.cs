using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROGP2.Models;
using System.Security.Claims;

namespace PROGP2.Controllers
{
    [Authorize(Policy = "FarmerOnly")]
    public class FarmerController : Controller
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();
        public IActionResult Index()
        {
            var userIDClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
               
                return RedirectToAction("Login", "Account");
            }

            var userID = int.Parse(userIDClaim);
            var products = context.Products.Where(p => p.UserId == userID).ToList();
            return View(products);
        
    }

        public IActionResult AddProduct()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
          
                product.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
          
           
        }
    }
}

