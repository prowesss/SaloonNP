using Microsoft.AspNetCore.Mvc;
using Saloon.Data;
using Saloon.Models.ServiceManagementModels;
using Microsoft.EntityFrameworkCore;
using Saloon.Models.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;

using Saloon.Areas.Identity.Data;

namespace Saloon.Controllers
{

    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Product.ToListAsync();
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel addProduct)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = addProduct.Name,
                    Description = addProduct.Description,
                    Price = addProduct.Price,
                    CostPrice = addProduct.CostPrice,
                    ExpireDate = addProduct.ExpireDate,
                    IsDeleted = addProduct.IsDeleted,
                    ImageURL = addProduct.ImageURL
                };
                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var product = _context.Product.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel editProduct = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CostPrice = product.CostPrice,
                ExpireDate = product.ExpireDate,
                IsDeleted = product.IsDeleted,
                ImageURL = product.ImageURL
            };

            return View(editProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)

            {
                try
                {
                    Product editedProduct = new Product()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        CostPrice = product.CostPrice,
                        ExpireDate = product.ExpireDate,
                        IsDeleted = product.IsDeleted,
                        ImageURL = product.ImageURL,

                };
                    _context.Update(editedProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (ProductExists(product.Id))
                    {
                        throw;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            return View(product);
        }
        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(x => x.Id == id);
        }
    }
}


