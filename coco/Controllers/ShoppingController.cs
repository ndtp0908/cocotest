using coco.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace YourNamespace.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly CocopureV1Context _context;

        public ShoppingController(CocopureV1Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Shopping()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Shopping([FromBody] OrderForm order)
        {
            if (order == null || order.Cart == null || order.Cart.Count == 0)
            {
                return BadRequest(new { success = false, message = "Giỏ hàng trống!" });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                string billId = Guid.NewGuid().ToString();
                string userId = order.UserId ?? "G+" + Guid.NewGuid().ToString();

                decimal total = order.Cart.Sum(i => i.ItemAmount * i.ItemPrice);
                decimal discount = 0m;
                decimal totalAfterDiscount = total * (1 - discount / 100);

                var bill = new Bill
                {
                    BillId = billId,
                    UserId = userId,
                    Discount = discount.ToString(),
                    EndAddress = order.Address,
                    PaymentMethod = order.PaymentMethod,
                    Total = totalAfterDiscount,
                    Status = "Đang xử lý",
                    DayBought = DateOnly.FromDateTime(DateTime.Now),
                };

                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();

                var billDetailsList = order.Cart.Select(item => new BillDetail
                {
                    BillId = billId,
                    ItemId = item.ItemId,
                    ItemCount = item.ItemAmount,
                    Price = item.ItemPrice
                }).ToList();

                await _context.BillDetails.AddRangeAsync(billDetailsList);
                await _context.SaveChangesAsync();

                foreach (var item in order.Cart)
                {
                    var storageItem = await _context.Storages.FindAsync(item.ItemId);
                    if (storageItem != null && storageItem.ItemAmount >= item.ItemAmount)
                    {
                        storageItem.ItemAmount -= item.ItemAmount;
                        _context.Storages.Update(storageItem);
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = $"Sản phẩm {item.ItemId} không đủ hàng!" });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Json(new { success = true, message = "Đặt hàng thành công!", billId = bill.BillId });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = "Lỗi khi lưu đơn hàng!", error = ex.Message });
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
