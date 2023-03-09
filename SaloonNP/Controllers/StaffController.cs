using Microsoft.AspNetCore.Mvc;
using SaloonNP.Data;
using SaloonNP.Models.UserManagementModels;
using Microsoft.EntityFrameworkCore;
using SaloonNP.Models.ViewModels.Staff;

namespace SaloonNP.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var staffs = await _context.Staff.Where(x => x.IsDeleted == false).ToListAsync();
            ViewData["Title"] = "List of Staffs";
            return View(staffs);
        }

        //Create
        [HttpGet]
        public ActionResult<Staff> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StaffViewModel createStaff)
        {
            if (ModelState.IsValid)
            {
                createStaff.Id = Guid.NewGuid();
                Staff newStaff = new Staff()
                {
                    Id = createStaff.Id,
                    ProfilePictureURL = createStaff.ImageUrl,
                    Description = createStaff.Description,
                    FullName = createStaff.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Prowess",
                    IsActive = createStaff.IsActive
                };
                _context.Staff.Add(newStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createStaff);
        }
        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            StaffViewModel editableStaff = new StaffViewModel()
            {
                Id = staff.Id,
                ImageUrl = staff.ProfilePictureURL,
                Description = staff.Description,
                Name = staff.FullName,
                IsActive = staff.IsActive,
                IsDeleted = staff.IsDeleted
            };

            return View(editableStaff);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, StaffViewModel staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Staff editedStaff = new Staff()
                    {
                        Id = staff.Id,
                        ProfilePictureURL = staff.ImageUrl,
                        Description = staff.Description,
                        FullName = staff.Name,
                        IsActive = staff.IsActive
                    };
                    _context.Update(editedStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (StaffExists(staff.Id))
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
            return View(staff);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted == false);

            if (staff == null)
            {
                return NotFound();
            }

            StaffViewModel deletableStaff = new StaffViewModel()
            {
                Id = staff.Id,
                ImageUrl = staff.ProfilePictureURL,
                Description = staff.Description,
                Name = staff.FullName,
                IsActive = staff.IsActive,
                IsDeleted = staff.IsDeleted
            };

            return View(deletableStaff);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted == false);

            if (staff != null)
            {
                staff.IsDeleted = true;
                _context.Update(staff);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted == false);

            if (staff == null)
            {
                return NotFound();
            }
            StaffViewModel viewableStaff = new StaffViewModel()
            {
                Id = staff.Id,
                ImageUrl = staff.ProfilePictureURL,
                Description = staff.Description,
                Name = staff.FullName,
                IsActive = staff.IsActive,
                IsDeleted = staff.IsDeleted
            };

            return View(viewableStaff);
        }

        private bool StaffExists(Guid id)
        {
            return _context.Staff.Any(e => e.Id == id && e.IsDeleted == false);
        }
    }
}
