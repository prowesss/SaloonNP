using Saloon.Models.UserManagementModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Saloon.Models.ServiceManagementModels
{
    public class Appointment : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter an appointment date.")]
        [Display(Name = "Appointment Date")]
        [FutureDate(ErrorMessage = "Appointment date must be in the future.")]
        public DateTime AppointmentDate { get; set; }

        [StringLength(500, ErrorMessage = "Notes must be less than 500 characters.")]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public bool IsCancelled { get; set; }
        public string? CancelledBy { get; set; }
        public DateTime? CancelledOn { get; set; }

        //Relationships
        public List<Appointment_Product> Appointments_Products { get; set; }

        [Required(ErrorMessage = "Please select a staff member.")]
        public Guid StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }

        [Required(ErrorMessage = "Please select a hairstyle.")]
        public Guid HairstyleId { get; set; }
        [ForeignKey("HairstyleId")]
        public HairStyle HairStyle { get; set; }

        [Required(ErrorMessage = "Please select a location.")]
        public Guid LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public Appointment()
        {
            IsActive = true;
            IsDeleted = false;
            IsCancelled = false;
        }
        public class FutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime dateTime = Convert.ToDateTime(value);
                return dateTime >= DateTime.Today;
            }
        }

    }
}
