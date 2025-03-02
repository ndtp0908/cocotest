using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using coco.Models;
using System.Collections.Generic;

namespace coco.Controllers
{
    public class ProductController : Controller
    {
        private readonly CocopureV1Context _context;

        public ProductController(CocopureV1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Storages.ToListAsync();
            return View("Product", products);
        }
    }
}
