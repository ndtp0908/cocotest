using coco.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coco.Controllers
{
    public class UserController : Controller
    {
        private readonly CocopureV1Context _context;

        public UserController(CocopureV1Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Update(string userId, string name, string userName, string email, DateOnly birthday,
            string gender, string address, string phone)
        {
            var userInfo = await _context.UserInfos.FindAsync(userId);
            if (userInfo == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng!";
                return RedirectToAction("Index", "User");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userInfo.Name = name;
                    userInfo.Email = email;
                    userInfo.Birthday = birthday;
                    userInfo.Gender = gender;
                    userInfo.Address = address;
                    userInfo.Phone = phone;

                    _context.Update(userInfo);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("Lỗi cập nhật dữ liệu: " + ex.Message);
                    TempData["ErrorMessage"] = "Lỗi khi cập nhật dữ liệu!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ!";
            }

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string userId, string oldPassword, string newPassword, string rePassword)
        {
            if (newPassword != rePassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu nhập lại không khớp!";
                return RedirectToAction("Index", "User");
            }

            var account = await _context.Users.FindAsync(userId);
            if (account == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy tài khoản!";
                return RedirectToAction("Index", "User");
            }

            if(account.PassWord != oldPassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu cũ không đúng!";
                return RedirectToAction("Index", "User");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    account.PassWord = newPassword;
                    _context.Update(account);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("Lỗi cập nhật dữ liệu: " + ex.Message);
                    TempData["ErrorMessage"] = "Lỗi khi đổi mật khẩu!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ!";
            }

            return RedirectToAction("Index", "User");
        }
    }
}
