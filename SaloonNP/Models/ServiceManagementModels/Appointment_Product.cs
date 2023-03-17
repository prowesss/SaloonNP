using System.ComponentModel.DataAnnotations.Schema;

namespace SaloonNP.Models.ServiceManagementModels
{
    public class Appointment_Product
    {
        public Guid AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
