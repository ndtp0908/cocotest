using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coco.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;

namespace coco.Controllers
{
    public class AccountController : Controller
    {
        private readonly CocopureV1Context _context;

        public AccountController(CocopureV1Context context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null || user.PassWord != password)
            {
                ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng!";
                return View();
            }
            var userTempInfo = await _context.UserInfos.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            var userInfo = new
            {
                userId = user.UserId,
                userName = user.UserName,
                name = userTempInfo.Name,
                phone = userTempInfo.Phone,
                email = userTempInfo.Email,
                address = userTempInfo.Address,
                role = userTempInfo.Role,
            };

            return Json(new { success = true, user = userInfo });
            //return RedirectToAction("Index", "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string fullName, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu không khớp!";
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}

