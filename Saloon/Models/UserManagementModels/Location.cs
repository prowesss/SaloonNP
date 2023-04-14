using Saloon.Models.ServiceManagementModels;
using System.ComponentModel.DataAnnotations;

namespace Saloon.Models.UserManagementModels
{
    public class Location : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "PostalCode")]
        public string PostalCode { get; set; }

        //Relationships
        public List<HairStyle> HairStyles { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Staff> Staffs { get; set; }

        public Location()
        {
            IsActive = true;
            IsDeleted = false;
        }

    }
}
