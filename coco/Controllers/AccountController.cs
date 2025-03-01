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
                birthday = userTempInfo.Birthday.ToString(),
                gender = userTempInfo.Gender,
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
        public async Task<IActionResult> SignUp(string fullName, string email, string phone, string address, DateOnly birthday,
    string gender, string userName, string password, string confirmPassword)
        {
            ViewBag.FullName = fullName;
            ViewBag.Email = email;
            ViewBag.Phone = phone;
            ViewBag.Address = address;
            ViewBag.Birthday = birthday.ToString("yyyy-MM-dd");
            ViewBag.Gender = gender;
            ViewBag.UserName = userName;

            if (password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu không khớp!";
                return View();
            }

            bool isUserNameExists = await _context.Users.AnyAsync(u => u.UserName == userName);
            bool isEmailExists = await _context.UserInfos.AnyAsync(u => u.Email == email);

            if (isUserNameExists)
            {
                ViewBag.Error = "Tên tài khoản đã tồn tại!";
                return View();
            }

            if (isEmailExists)
            {
                ViewBag.Error = "Email đã được sử dụng!";
                return View();
            }

            var userId = Guid.NewGuid().ToString();

            var account = new User
            {
                UserId = userId,
                UserName = userName,
                PassWord = password,
            };

            var userInfo = new UserInfo
            {
                UserId = userId,
                Name = fullName,
                Phone = phone,
                Email = email,
                Address = address,
                Gender = gender,
                Birthday = birthday,
                Role = "User"
            };

            _context.Users.Add(account);
            _context.UserInfos.Add(userInfo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

    }
}

