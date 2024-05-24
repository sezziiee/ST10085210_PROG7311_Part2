using Microsoft.AspNetCore.Mvc;

namespace PROGP2.Controllers
{
    public class Employee : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
