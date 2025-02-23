using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using session3task.Data;
using session3task.Models;


namespace session3task.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = new Company { Name = model.CompanyName };
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                var product = new Product
                {
                    Name = model.ProductName,
                    Price = model.Price,
                    CompanyId = company.Id
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = product.Id });
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Company)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }
    }
}