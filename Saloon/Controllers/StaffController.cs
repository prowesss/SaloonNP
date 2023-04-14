using Microsoft.AspNetCore.Mvc;
using Saloon.Data;
using Saloon.Models.UserManagementModels;
using Microsoft.EntityFrameworkCore;
using Saloon.Models.ViewModels.Staff;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Saloon.Areas.Identity.Data;

namespace Saloon.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var staffs = await _context.Staff.Where(x => x.IsDeleted == false).ToListAsync();
            ViewData["Title"] = "List of Staffs";
            return View(staffs);
        }

        [Authorize(Roles = "Admin")]
        //Create
        [HttpGet]
        public ActionResult<Staff> Create()
        {
            var location = _context.Location;
            var locationSelectList = new List<SelectListItem>();
            foreach (var item in location)
            {
                locationSelectList.Add(new SelectListItem(item.Address, item.Id.ToString()));
            }
            StaffViewModel viewModel = new StaffViewModel();
            viewModel.Locations = locationSelectList;
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(StaffViewModel createStaff)
        {
            ModelState.Remove("Locations");
            if (ModelState.IsValid)
            {
                createStaff.Id = Guid.NewGuid();
                var location = await _context.Location.FirstOrDefaultAsync(x => x.Id == createStaff.LocationId);
                Staff newStaff = new Staff()
                {
                    
                    ProfilePictureURL = createStaff.ImageUrl,
                    Description = createStaff.Description,
                    FullName = createStaff.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Prowess",
                    IsActive = createStaff.IsActive,
                    Location = location
                };
                _context.Staff.Add(newStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createStaff);
        }

        [Authorize(Roles = "Admin")]
        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff =  await _context.Staff.Include(x => x.Location).FirstOrDefaultAsync(y => y.Id == id);
            if (staff == null)
            {
                return NotFound();
            }
            var location = _context.Location;
            var locationSelectList = new List<SelectListItem>();
            foreach (var item in location)
            {
                locationSelectList.Add(new SelectListItem(item.Address, item.Id.ToString()));
            }
            StaffViewModel editableStaff = new StaffViewModel()
            {
                Id = staff.Id,
                ImageUrl = staff.ProfilePictureURL,
                Description = staff.Description,
                Name = staff.FullName,
                IsActive = staff.IsActive,
                IsDeleted = staff.IsDeleted,
                LocationId = staff.LocationId,
                Locations = locationSelectList,
            };
           
            return View(editableStaff);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, StaffViewModel staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Locations");

            if (ModelState.IsValid)
            {
                try
                {
                    var location = await _context.Location.FirstOrDefaultAsync(x => x.Id == staff.LocationId);
                    Staff editedStaff = new Staff()
                    {
                        Id = staff.Id,
                        ProfilePictureURL = staff.ImageUrl,
                        Description = staff.Description,
                        FullName = staff.Name,
                        IsActive = staff.IsActive,
                        Location = location
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


        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
