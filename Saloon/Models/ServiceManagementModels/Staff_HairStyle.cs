using Saloon.Models.UserManagementModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saloon.Models.ServiceManagementModels
{
    public class Staff_HairStyle
    {
        public Guid StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }

        public Guid HairStyleId { get; set; }
        [ForeignKey("HairStyleId")]
        public HairStyle HairStyle { get; set; }
    }
}
