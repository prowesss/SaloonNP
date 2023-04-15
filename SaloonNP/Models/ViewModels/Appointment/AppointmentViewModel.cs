
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SaloonNP.Models.ViewModels.Appointment
{
    public class AppointmentViewModel
    {
        public Guid? Id { get; set; }
        //Reviewer Umesh 20220860 : DateTime appointmentdate varibale should be AppointmentDate 
        public DateTime appointmentdate { get; set; }
        public string Notes { get; set; }
        public bool IsCancelled { get; set; }
        public string ProductIds { get; set; }
        public Guid StaffId { get; set; }
        public Guid LocationId { get; set; }
        //Reviewer Umesh 20220860: hairstyleId should be declared as HairStyleId
        public Guid hairstyleId { get; set; }
        public IEnumerable<SaloonNP.Models.ServiceManagementModels.HairStyle>? HairStyles { get; set; }
        public List<SaloonNP.Models.ServiceManagementModels.Product>? Products { get; set; }
        public List<SelectListItem>? Locations { get; set; }
        public List<SaloonNP.Models.UserManagementModels.Staff>? Staffs { get; set; }

    }
}
