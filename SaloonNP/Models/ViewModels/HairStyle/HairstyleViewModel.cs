using Microsoft.AspNetCore.Mvc.Rendering;
using SaloonNP.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SaloonNP.Models.ViewModels.HairStyle
{
    public class HairstyleViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double? Price { get; set; }

        public string? ImageURL { get; set; }

        public Gender? Gender { get; set; }

        public IReadOnlyList<SelectListItem> Locations { get; set; }

        public IReadOnlyList<SelectListItem> Staffs { get; set; }

        public List<Guid> StaffIds { get; set; }

        public Guid LocationId { get; set; }

        

    }
}
