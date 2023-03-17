using System.ComponentModel.DataAnnotations;

namespace SaloonNP.Models.ViewModels.Product
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public double? CostPrice { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
