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

        public DbSet<Product> Product { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<HairStyle> HairStyle { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Staff> Staff { get; set; }

        public DbSet<Staff_HairStyle> Staff_HairStyle { get; set; }
        public DbSet<Appointment_Product> Appointment_Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment_Product>()
                .HasKey(ap => new { ap.AppointmentId, ap.ProductId });

            modelBuilder.Entity<Appointment_Product>()
                .HasOne(ap => ap.Appointment)
                .WithMany(a => a.Appointments_Products)
                .HasForeignKey(ap => ap.AppointmentId);

            modelBuilder.Entity<Appointment_Product>()
                .HasOne(ap => ap.Product)
                .WithMany(p => p.Appointments_Products)
                .HasForeignKey(ap => ap.ProductId);

            modelBuilder.Entity<Staff_HairStyle>()
                .HasKey(sh => new { sh.StaffId, sh.HairStyleId });

            modelBuilder.Entity<Staff_HairStyle>()
                .HasOne(sh => sh.Staff)
                .WithMany(s => s.Staffs_HairStyles)
                .HasForeignKey(sh => sh.StaffId);

            modelBuilder.Entity<Staff_HairStyle>()
                .HasOne(sh => sh.HairStyle)
                .WithMany(h => h.Staffs_HairStyles)
                .HasForeignKey(sh => sh.HairStyleId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Staff)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StaffId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.HairStyle)
                .WithMany(h => h.Appointments)
                .HasForeignKey(a => a.HairstyleId);

            modelBuilder.Entity<HairStyle>()
                .HasOne(h => h.Location)
                .WithMany(l => l.HairStyles)
                .HasForeignKey(h => h.LocationId);

            modelBuilder.Entity<Staff>()
                .HasOne(h => h.Location)
                .WithMany(l => l.Staffs)
                .HasForeignKey(h => h.LocationId);

            modelBuilder.Entity<Appointment>()
                .HasOne(h => h.Location)
                .WithMany(l => l.Appointments)
                .HasForeignKey(h => h.LocationId);
        }
    }
}