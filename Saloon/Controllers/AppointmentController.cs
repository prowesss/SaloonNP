using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Saloon.Areas.Identity.Data;
using Saloon.Models.ServiceManagementModels;
using Saloon.Models.ViewModels.Appointment;

namespace Saloon.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var appointment = _context.Appointment
                .Include(x => x.Location)
                .Include(x => x.HairStyle)
                .Include(x => x.Staff)
                .Include(x => x.Appointments_Products).ThenInclude(y => y.Product);
            return View(appointment);
        }

        [HttpGet]
        public ActionResult<Appointment> Create()
        {
            var staff = _context.Staff.ToList();
            var staffSelectList = new List<SelectListItem>();
            foreach (var item in staff)
            {
                staffSelectList.Add(new SelectListItem(item.FullName, item.Id.ToString()));
            }

            var location = _context.Location.ToList(); ;
            var locationSelectList = new List<SelectListItem>();

            foreach (var item in location)
            {
                locationSelectList.Add(new SelectListItem(item.Address, item.Id.ToString()));
            }

            var hairStyle = _context.HairStyle.ToList(); ;
            var hairStyleSelectList = new List<SelectListItem>();

            foreach (var item in hairStyle)
            {
                hairStyleSelectList.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }

            var product = _context.Product.ToList(); ;
            var productSelectList = new List<SelectListItem>();

            foreach (var item in product)
            {
                productSelectList.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }


            AppointmentViewModel viewModel = new AppointmentViewModel();
            viewModel.Staffs = staff;
            viewModel.Locations = locationSelectList;
            viewModel.Products = product;
            viewModel.HairStyles = hairStyle;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cart([FromBody] AppointmentViewModel appointment)
        {
            AppointmentViewModel viewModel = new AppointmentViewModel();
            viewModel.ProductIds = appointment.ProductIds;
            viewModel.StaffId = appointment.StaffId;
            viewModel.HairstyleId = appointment.HairstyleId;
            viewModel.LocationId = appointment.LocationId;
            viewModel.Notes = appointment.Notes;
            viewModel.IsCancelled = false;

            TempData["AppointmentBooking"] = viewModel;
            string json = JsonConvert.SerializeObject(viewModel);
            return Ok(json);
        }

        public IActionResult Cart(string appointment)
        {
            if (string.IsNullOrEmpty(appointment))
            {
                // Return an error view if the appointment parameter is missing or empty
                return View("Error");
            }
            try
            {
                var staff = _context.Staff.ToList();
                var location = _context.Location.ToList();
                var locationSelectList = new List<SelectListItem>();

                foreach (var item in location)
                {
                    locationSelectList.Add(new SelectListItem(item.Address, item.Id.ToString()));
                }

                var hairStyle = _context.HairStyle.ToList();
                var product = _context.Product.ToList();
                var appointmentModel = JsonConvert.DeserializeObject<AppointmentViewModel>(appointment);

                AppointmentViewModel viewModel = new AppointmentViewModel()
                {
                    Id = Guid.NewGuid(),
                    AppointmentDate = DateTime.Now,
                    HairstyleId = appointmentModel.HairstyleId,
                    HairStyles = hairStyle,
                    ProductIds = appointmentModel.ProductIds,
                    Products = product,
                    StaffId = appointmentModel.StaffId,
                    Staffs = staff,
                    LocationId = appointmentModel.LocationId,
                    Locations = locationSelectList,
                    Notes = appointmentModel.Notes,
                    IsCancelled = false,
                };
            
                // Deserialize the appointment view model from the JSON string
               
              
                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the error and return an error view
                
                return View("Error");
            }

        }


        [HttpPost]
        public async Task<ActionResult> Create(AppointmentViewModel appointmentViewModel)
        {
            ModelState.Remove("Staffs");
            ModelState.Remove("Locations");
            ModelState.Remove("Hairstyles");
            ModelState.Remove("Products");

            if (ModelState.IsValid)
            {
                appointmentViewModel.Id = Guid.NewGuid();
                var productIds = appointmentViewModel.ProductIds.Split(',');
                var location = await _context.Location.FirstOrDefaultAsync(x => x.Id == appointmentViewModel.LocationId);
                var harstyle = await _context.HairStyle.FirstOrDefaultAsync(x => x.Id == appointmentViewModel.HairstyleId);
                var staff = await _context.Staff.FirstOrDefaultAsync(x => x.Id == appointmentViewModel.StaffId);
                var products = _context.Product.Where(x => productIds.Contains(x.Id.ToString()))
                    .ToList();
                var appointmentProducts = new List<Appointment_Product>();
                foreach (var product in products)
                {
                    appointmentProducts.Add(new Appointment_Product
                    {
                        Product = product
                    });
                }
                var book = new Appointment
                {
                    Id = appointmentViewModel.Id.Value,
                    AppointmentDate = appointmentViewModel.AppointmentDate,
                    Notes = appointmentViewModel.Notes,
                    IsActive = true,
                    IsCancelled = false,
                    HairStyle = harstyle,
                    Staff = staff,
                    Location = location,
                    Appointments_Products = appointmentProducts
                };

                _context.Appointment.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentViewModel);
        }

        public async Task<ActionResult> AddAppointment()
        {
            var myDataJson = HttpContext.Session.GetString("AppointmentBooking");
            var myData = JsonConvert.DeserializeObject<AppointmentViewModel>(myDataJson);
            var productIds = myData.ProductIds.Split(',');
            myData.Id = Guid.NewGuid();
            var location = await _context.Location.FirstOrDefaultAsync(x => x.Id == myData.LocationId);
            var harstyle = await _context.HairStyle.FirstOrDefaultAsync(x => x.Id == myData.HairstyleId);
            var staff = await _context.Staff.FirstOrDefaultAsync(x => x.Id == myData.StaffId);
            var products = _context.Product.Where(x => productIds.Contains(x.Id.ToString()))
                .ToList();
            var appointmentProducts = new List<Appointment_Product>();
            foreach (var product in products)
            {
                appointmentProducts.Add(new Appointment_Product
                {
                    Product = product
                });
            }
            var book = new Appointment
            {
                Id = myData.Id.Value,
                AppointmentDate = myData.AppointmentDate,
                Notes = myData.Notes,
                IsActive = true,
                IsCancelled = false,
                HairStyle = harstyle,
                Staff = staff,
                Location = location,
                Appointments_Products = appointmentProducts
            };

            _context.Appointment.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
