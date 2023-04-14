using System.ComponentModel.DataAnnotations;

namespace Saloon.Models.ServiceManagementModels
{
    public class Product : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "ImageURL")]
        public string ImageURL { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public double? Price { get; set; }

        [Display(Name = "Cost Price")]
        public double? CostPrice { get; set; }

        [Display(Name = "Expire Date")]
        public DateTime ExpireDate { get; set; }
        //Relationships
        public List<Appointment_Product> Appointments_Products { get; set; }

        public Product()
        {
            IsActive = true;
            IsDeleted = false;
        }

    }
}
