﻿using SaloonNP.Models.ServiceManagementModels;
namespace SaloonNP.Models
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public bool IsDeleted  { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

     
    }
}
