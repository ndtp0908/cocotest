using coco.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace coco.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View("~/Views/About/About.cshtml");
        }

        public IActionResult Product()
        {
            return View("~/Views/Product/Product.cshtml");
        }

        public IActionResult Shopping()
        {
            return View("~/Views/Shopping/Shopping.cshtml");
        }

        public IActionResult User()
        {
            return View("~/Views/User/User.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
