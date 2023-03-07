using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaloonNP.Models.ServiceManagementModels;
using SaloonNP.Models.UserManagementModels;

namespace SaloonNP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff_HairStyle>().HasKey(am => new
            {
                am.StaffId,
                am.HairStyleId
            });

            modelBuilder.Entity<Staff_HairStyle>().HasOne(m => m.Staff).WithMany(am => am.Staffs_HairStyles).HasForeignKey(m => m.HairStyleId);
            modelBuilder.Entity<Staff_HairStyle>().HasOne(m => m.HairStyle).WithMany(am => am.Staffs_HairStyles).HasForeignKey(m => m.StaffId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<HairStyle> HairStyle { get; set; }
        public DbSet<Staff_HairStyle> Staff_HairStyle { get; set; }
        public DbSet<Location> Location { get; set; }
    }
}