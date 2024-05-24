using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PROGP2.Controllers
{
    [Authorize(Policy = "FarmerOnly")]
    public class FarmerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
