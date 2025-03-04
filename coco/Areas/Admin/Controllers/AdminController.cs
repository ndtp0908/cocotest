using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coco.Models;

namespace coco.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly CocopureV1Context _context;

        public AdminController(CocopureV1Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string userId, string oldPassword, string newPassword, string rePassword)
        {
            if (newPassword != rePassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu nhập lại không khớp!";
                return RedirectToAction("Index", "Home");
            }

            var account = await _context.Users.FindAsync(userId);
            if (account == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy tài khoản!";
                return RedirectToAction("Index", "Home");
            }

            if (account.PassWord != oldPassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu cũ không đúng!";
                return RedirectToAction("Index", "Home");
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

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Storages
                .Select(p => new
                {
                    p.ItemId,
                    p.ItemName,
                    p.ItemPrice,
                    p.ItemAmount
                })
                .ToListAsync();

            return Json(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.UserInfos
                .Select(u => new
                {
                    u.UserId,
                    u.Name,
                    u.Phone,
                    u.Gender,
                    u.Birthday,
                    u.Address,
                    u.Email,
                    u.Role,
                    u.User.UserName
                })
                .ToListAsync();

            return Json(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Storage newProduct)
        {
            if (newProduct == null || string.IsNullOrWhiteSpace(newProduct.ItemName))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            newProduct.ItemId = Guid.NewGuid().ToString();
            _context.Storages.Add(newProduct);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Thêm sản phẩm thành công!" });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserInfoAdmin newUser)
        {
            if (newUser == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }
            var oldUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == newUser.UserName);
            if (oldUser != null)
            {
                return BadRequest(new { message = "Tên đăng nhập đã tồn tại!" });
            };
            newUser.UserId = Guid.NewGuid().ToString();
            var user = new User
            {
                UserId = newUser.UserId,
                UserName = newUser.UserName,
                PassWord = newUser.Password
            };
            var userInfo = new UserInfo
            {
                UserId = newUser.UserId,
                Name = newUser.Name,
                Phone = newUser.Phone,
                Email = newUser.Email,
                Address = newUser.Address,
                Birthday = newUser.Birthday,
                Gender = newUser.Gender,
                Role = newUser.Role
            };
            _context.Users.Add(user);
            _context.UserInfos.Add(userInfo);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Thêm sản phẩm thành công!" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserInfoAdmin user)
        {
            if (string.IsNullOrEmpty(user.UserId))
            {
                return BadRequest(new { message = "ID không hợp lệ!" });
            }

            var updateUser = await _context.UserInfos.FindAsync(user.UserId);
            if (updateUser == null)
            {
                return NotFound(new { message = "Không tìm thấy người dùng!" });
            }

            bool isUserNameTaken = await _context.Users
                .AnyAsync(u => u.UserName == user.UserName && u.UserId != user.UserId);

            if (isUserNameTaken)
            {
                return BadRequest(new { message = "Tên đăng nhập đã tồn tại!" });
            }

            updateUser.Address = user.Address;
            updateUser.Birthday = user.Birthday;
            updateUser.Email = user.Email;
            updateUser.Name = user.Name;
            updateUser.Phone = user.Phone;
            updateUser.Role = user.Role;
            updateUser.Gender = user.Gender;

            var userAccount = await _context.Users.FindAsync(user.UserId);
            if (userAccount == null)
            {
                return NotFound(new { message = "Tài khoản không tồn tại!" });
            }

            userAccount.UserName = user.UserName;
            userAccount.PassWord = user.Password;

            _context.Users.Update(userAccount);
            _context.UserInfos.Update(updateUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cập nhật người dùng thành công!" });
        }

    [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Storage updatedProduct)
        {
            if (string.IsNullOrEmpty(updatedProduct.ItemId))
            {
                return BadRequest(new { message = "ID sản phẩm không hợp lệ!" });
            }

            var product = await _context.Storages.FindAsync(updatedProduct.ItemId);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }

            product.ItemName = updatedProduct.ItemName;
            product.ItemPrice = updatedProduct.ItemPrice;
            product.ItemAmount = updatedProduct.ItemAmount;

            _context.Storages.Update(product);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Cập nhật sản phẩm thành công!" });
        }
    }
}
