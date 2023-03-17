using Catel.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SaloonNP.Models.ViewModels.Staff
{
    public class StaffViewModel
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LocationId { get; set; }
        public IReadOnlyList<SelectListItem> Locations { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


    }
}
