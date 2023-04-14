
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Saloon.Models.ViewModels.Appointment
{
    public class AppointmentViewModel
    {
        public Guid? Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
        public bool IsCancelled { get; set; }
        public string ProductIds { get; set; }
        public Guid StaffId { get; set; }
        public Guid LocationId { get; set; }
        public Guid HairstyleId { get; set; }
        public IEnumerable<Saloon.Models.ServiceManagementModels.HairStyle>? HairStyles { get; set; }
        public List<Saloon.Models.ServiceManagementModels.Product>? Products { get; set; }
        public List<SelectListItem>? Locations { get; set; }
        public List<Saloon.Models.UserManagementModels.Staff>? Staffs { get; set; }

    }
}
