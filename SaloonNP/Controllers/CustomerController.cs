//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SaloonNP.Data;
//using SaloonNP.Data.Enums;
//using SaloonNP.Models.UserManagementModels;

//namespace SaloonNP.Controllers
//{
//    public class CustomerController : Controller
//    {
//        private readonly UserManager<IdentityUser> _userManager;

//        public CustomerController(UserManager<IdentityUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        // GET: Customer
//        public async Task<IActionResult> Index()
//        {
//            var usersInRole = await _userManager.GetUsersInRoleAsync(RoleEnum.Customer.ToString());

//            // Do something with the list of users, such as returning them as a response
//            return View(usersInRole);
//        }

//        // GET: Customer/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Customer/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Name,Email")] Customer customer)
//        {
//            if (ModelState.IsValid)
//            {
//                customer.Id = Guid.NewGuid();
//                _context.Add(customer);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(customer);
//        }

//        // GET: Customer/Edit/5
//        public async Task<IActionResult> Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var customer = await _context.Customers.FindAsync(id);
//            if (customer == null)
//            {
//                return NotFound();
//            }
//            return View(customer);
//        }

//        // POST: Customer/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email")] Customer customer)
//        {
//            if (id != customer.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(customer);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!CustomerExists(customer.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(customer);
//        }

//        // GET: Customer/Delete/5
//        public async Task<IActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
//            if (customer == null)
//            {
//                return NotFound();
//            }

//            return View(customer);
//        }

//        // POST: Customer/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(Guid id)
//        {
//            var customer = await _context.Customers.FindAsync(id);
//            _context.Customers.Remove(customer);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool CustomerExists(Guid id)
//        {
//            return _context.Customers.Any(e => e.Id == id);
//        }
//    }

//}
