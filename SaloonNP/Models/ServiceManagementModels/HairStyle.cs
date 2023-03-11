using SaloonNP.Data.Enums;
using SaloonNP.Models.UserManagementModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaloonNP.Models.ServiceManagementModels
{
    public class HairStyle : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public double? Price { get; set; }

        [Display(Name = "ImageURL")]
        public string? ImageURL { get; set; }

        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        //Relationships
        public List<Staff_HairStyle> Staffs_HairStyles { get; set; }

        public Guid LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }


        public HairStyle()
        {
            IsActive = true;
            IsDeleted = false;
        }
    }
}
