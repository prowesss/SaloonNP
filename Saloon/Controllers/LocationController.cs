using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Saloon.Areas.Identity.Data;
using Saloon.Models.UserManagementModels;
using Saloon.Models.ViewModels.Location;

using Saloon.Areas.Identity.Data;


namespace Saloon.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var locations = await _context.Location.ToListAsync();
            return View(locations);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<Staff> Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(LocationViewModel addLocation)
        {
            if (ModelState.IsValid)
            {
                addLocation.Id = Guid.NewGuid();
                Location newLocation = new Location()
                {

                    Address = addLocation.Address,
                    PhoneNumber = addLocation.PhoneNumber,
                    City = addLocation.City,
                    Country = addLocation.Country,
                    PostalCode = addLocation.PostalCode
                };

                _context.Location.Add(newLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(addLocation);
        }
        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = _context.Location.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            LocationViewModel editableLocation = new LocationViewModel
            {
                Id = location.Id,
                Address = location.Address,
                PhoneNumber = location.PhoneNumber,
                City = location.City,
                Country = location.Country,
                PostalCode = location.PostalCode
            };

            return View(editableLocation);
        }

        [Authorize(Roles = "Admin")]
        // POST: Location/Edit/5
        [HttpPost]

        public async Task<IActionResult> Edit(Guid id, LocationViewModel location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Location editedlocation = new Location()
                    {
                        Id = location.Id,
                        Address = location.Address,
                        PhoneNumber = location.PhoneNumber,
                        City = location.City,
                        Country = location.Country,
                        PostalCode = location.PostalCode

                    };
                    _context.Update(editedlocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (LocationExists(location.Id))
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

            return View(location);
        }

        [Authorize(Roles = "Admin")]
        // GET: Location/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var location = _context.Location.FirstOrDefault(l => l.Id == id);

            if (location == null)
            {
                return NotFound();
            }
            LocationViewModel deletableLocation = new LocationViewModel()
            {
                Id = location.Id,
                Address = location.Address,
                PhoneNumber = location.PhoneNumber,
                City = location.City,
                Country = location.Country,
                PostalCode = location.PostalCode,
                IsDeleted = location.IsDeleted
            };

            return View(deletableLocation);
        }

        [Authorize(Roles = "Admin")]
        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var location = await _context.Location.FirstOrDefaultAsync(l => l.Id == id);

            if (location != null)
            {
                location.IsDeleted = true;
                _context.Update(location);
                await _context.SaveChangesAsync();
            }



            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(Guid id)
        {
            return _context.Location.Any(x => x.Id == id);
        }
    }
}

