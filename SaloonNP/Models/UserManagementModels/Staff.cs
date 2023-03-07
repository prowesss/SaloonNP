﻿using System.ComponentModel.DataAnnotations;
using SaloonNP.Models.ServiceManagementModels;

namespace SaloonNP.Models.UserManagementModels
{
    public class Staff : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Profile Picture URL")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        //relationships
        public List<Staff_HairStyle> Staffs_HairStyles { get; set; }

        public Staff()
        {
            IsActive = true;
            IsDeleted = false;
        }

    }
}
