using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class ShoppingController : Controller
    {
        [HttpGet]
        public IActionResult Shopping()
        {
            return View(); // Tự động tìm /Views/Shopping/Shopping.cshtml
        }

        [HttpPost]
        public IActionResult Shopping(string fullName, string phone, string address, string paymentMethod)
        {
            TempData["SuccessMessage"] = "Đặt hàng thành công! Chúng tôi sẽ liên hệ với bạn sớm.";
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
