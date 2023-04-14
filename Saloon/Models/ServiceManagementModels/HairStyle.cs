using Saloon.Data.Enums;
using Saloon.Models.UserManagementModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saloon.Models.ServiceManagementModels
{
    public class HairStyle : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Display(Name = "Price")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Please select a gender.")]
        [Display(Name = "ImageURL")]
        public string? ImageURL { get; set; }

        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        //Relationships
        public List<Staff_HairStyle> Staffs_HairStyles { get; set; }

        public Guid LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public List<Appointment> Appointments { get; set; }

        public HairStyle()
        {
            IsActive = true;
            IsDeleted = false;
            
        }
    }
}
