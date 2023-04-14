using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaloonNP.Data;
using SaloonNP.Data.Enums;
using SaloonNP.Models.ServiceManagementModels;
using SaloonNP.Models.UserManagementModels;
using SaloonNP.Models.ViewModels.HairStyle;


namespace SaloonNP.Controllers
{
    public class HairstyleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Hairstylecontroller(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var hairstyle = await _context.HairStyle
                    .Include(x=> x.Staffs_HairStyles)
                    .ThenInclude(y=>y.Staff).ToListAsync();
                return View(hairstyle);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<HairStyle> Create()
        {
            var staff = _context.Staff.ToList();
            var staffSelectList = new List<SelectListItem>();
            foreach (var item in staff)
            {
                staffSelectList.Add(new SelectListItem(item.FullName,item.Id.ToString()));
            }
            var location = _context.Location;
            var locationSelectList = new List<SelectListItem>();

            foreach (var item in location)
            {
                locationSelectList.Add(new SelectListItem(item.Address, item.Id.ToString()));
            }
            HairstyleViewModel viewModel = new HairstyleViewModel();
            viewModel.Staffs = staffSelectList;
            viewModel.Locations = locationSelectList;

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HairstyleViewModel addHairstyle)
        {
            ModelState.Remove("Staffs");
            ModelState.Remove("Locations");
            if (ModelState.IsValid)
            {
                addHairstyle.Id = Guid.NewGuid();
                var location = await _context.Location.FirstOrDefaultAsync(x => x.Id == addHairstyle.LocationId);
                
                HairStyle newHairstyle = new HairStyle()
                {
                    Name = addHairstyle.Name,
                    Description = addHairstyle.Description,
                    Price = addHairstyle.Price,
                    ImageURL = addHairstyle.ImageURL,
                    Gender = addHairstyle.Gender,
                    Location = location
                };

                _context.HairStyle.Add(newHairstyle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addHairstyle);
        }
        // GET: HairStyles/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hairStyle = await _context.HairStyle.Include(x => x.Location).FirstOrDefaultAsync(y => y.Id == id);

            if (hairStyle == null)
            {
                return NotFound();
            }

            var location = _context.Location;
            var locationSelectList = new List<SelectListItem>();
            foreach (var item in location)
            {
                locationSelectList.Add(new SelectListItem(item.Address, item.Id.ToString()));
            }
            HairstyleViewModel editableHairstyle = new HairstyleViewModel
            {
                Id = hairStyle.Id,
                Name = hairStyle.Name,
                Description = hairStyle.Description,
                Price = hairStyle.Price,
                ImageURL = hairStyle.ImageURL,
                Gender = hairStyle.Gender,
                StaffIds = hairStyle.Staffs_HairStyles.Select(sh => sh.StaffId).ToList(),
                Locations = locationSelectList,
            };

            return View(editableHairstyle);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, HairstyleViewModel hairstyle)
        {
            if (id != hairstyle.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Locations");


            if (ModelState.IsValid)
            {
                try
                {
                    var location = await _context.Location.FirstOrDefaultAsync(x => x.Id == hairstyle.LocationId);
                    HairStyle editedHairstyle = new HairStyle()
                    {
                        Id = hairstyle.Id,
                        Name = hairstyle.Name,
                        Description = hairstyle.Description,
                        Price = hairstyle.Price,
                        ImageURL = hairstyle.ImageURL,
                        Gender = hairstyle.Gender,
                        Location = location
                    };
                    _context.Update(editedHairstyle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (HairStyleExists(hairstyle.Id))
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
            return View(hairstyle);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var hairStyle = await _context.HairStyle.Include(h => h.Staffs_HairStyles).FirstOrDefaultAsync(h => h.Id == id);

            if (hairStyle == null)
            {
                return NotFound();
            }

            var viewModel = new HairstyleViewModel
            {
                Id = hairStyle.Id,
                Name = hairStyle.Name,
                Description = hairStyle.Description,
                Price = hairStyle.Price,
                ImageURL = hairStyle.ImageURL,
                Gender = hairStyle.Gender,
                StaffIds = hairStyle.Staffs_HairStyles.Select(sh => sh.StaffId).ToList(),
                LocationId = hairStyle.LocationId
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: HairStyles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var hairStyle = await _context.HairStyle
                .FirstOrDefaultAsync(m => m.Id == id && m.IsDeleted == false);
            
            if (hairStyle == null)
            {
                return NotFound();
            }

           HairstyleViewModel deletableHairstye = new HairstyleViewModel()
           {
               Name = hairStyle.Name,
               Description = hairStyle.Description,
               Price = hairStyle.Price,
               ImageURL = hairStyle.ImageURL,
               Gender = hairStyle.Gender,
               
           };

            return View(deletableHairstye);
        }

        // POST: HairStyles/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hairstyle = await _context.HairStyle.FirstOrDefaultAsync(h => h.Id == id && h.IsDeleted == false);

            if (hairstyle != null)
            {
                hairstyle.IsDeleted = true;
                _context.Update(hairstyle);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


        private bool HairStyleExists(Guid id)
        {
            return _context.HairStyle.Any(e => e.Id == id);
        }
    }
}

