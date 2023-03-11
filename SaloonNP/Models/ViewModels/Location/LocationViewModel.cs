using System.ComponentModel.DataAnnotations;

namespace SaloonNP.Models.ViewModels.Location
{
    public class LocationViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public bool IsDeleted { get; set; }
    }
}
