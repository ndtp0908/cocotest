using coco.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace coco.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CocopureV1Context _context;
        public HomeController(ILogger<HomeController> logger, CocopureV1Context context)
        {
            _logger = logger;
            _context = context;
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

        [HttpPost]
        public async Task<IActionResult> Box(string name, string phone, string email, string address, string note)
        {
            var user = await _context.NonCustomers.FirstOrDefaultAsync(b => b.Email == email);
            if (user != null)
            {
                TempData["ErrorMessage"] = "Bạn đã góp ý rồi!";
                return RedirectToAction("Index");
            }
            var id = Guid.NewGuid().ToString();
                var box = new NonCustomer
                {
                    UserId = id,
                    Name = string.IsNullOrWhiteSpace(name) ? "Trống" : name,
                    Email = string.IsNullOrWhiteSpace(email) ? "Trống" : email,
                    Phone = string.IsNullOrWhiteSpace(phone) ? "Trống" : phone,
                    Address = string.IsNullOrWhiteSpace(address) ? "Trống" : address,
                    Note = string.IsNullOrWhiteSpace(note) ? "Trống" : note,
                    DaySend = DateOnly.FromDateTime(DateTime.Now)
                };

                await _context.NonCustomers.AddAsync(box);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Gửi thông tin thành công!";
                return RedirectToAction("Index");
        }
    }
}
