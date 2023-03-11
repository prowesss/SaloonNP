using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaloonNP.Data;
using SaloonNP.Models.ServiceManagementModels;
using SaloonNP.Models.UserManagementModels;
using SaloonNP.Models.ViewModels.HairStyle;


namespace SaloonNP.Controllers
{
    public class HairstyleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HairstyleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var hairstyle = await _context.HairStyle.ToListAsync();
                return View(hairstyle);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet]
        public ActionResult<HairStyle> Create()
        {
            var staff = _context.Staff.ToList();
            var staffSelectList = new List<SelectListItem>();
            foreach (var item in staff)
            {
                staffSelectList.Add(new SelectListItem() { Text = item.FullName, Value = item.Id.ToString() });
            }

            ViewBag.StaffIds = staffSelectList;
            var location = _context.Location;
            ViewBag.Location = new SelectList(location, "Id", "Address");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HairstyleViewModel addHairstyle)
        {
            if (ModelState.IsValid)
            {
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
                
                var staffs = new List<Staff_HairStyle>();

                if (addHairstyle.StaffIds.Any())
                {
                    foreach (var item in addHairstyle.StaffIds)
                    {
                        var staff = await _context.Staff.FirstOrDefaultAsync(x => x.Id == item);
                        if (staff != null)
                        {
                            staffs.Add(new Staff_HairStyle { Staff = staff , HairStyle = newHairstyle});
                        }
                    }

                }
                _context.Staff_HairStyle.AddRange(staffs);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            return View(addHairstyle);
        }
    }
}
