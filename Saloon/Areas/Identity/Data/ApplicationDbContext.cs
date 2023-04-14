using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saloon.Areas.Identity.Data;
using Saloon.Models.ServiceManagementModels;
using Saloon.Models.UserManagementModels;
using System.Reflection.Emit;

namespace Saloon.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Appointment_Product>()
             .HasKey(ap => new { ap.AppointmentId, ap.ProductId });

        builder.Entity<Appointment_Product>()
            .HasOne(ap => ap.Appointment)
            .WithMany(a => a.Appointments_Products)
            .HasForeignKey(ap => ap.AppointmentId);

        builder.Entity<Appointment_Product>()
            .HasOne(ap => ap.Product)
            .WithMany(p => p.Appointments_Products)
            .HasForeignKey(ap => ap.ProductId);

        builder.Entity<Staff_HairStyle>()
            .HasKey(sh => new { sh.StaffId, sh.HairStyleId });

        builder.Entity<Staff_HairStyle>()
            .HasOne(sh => sh.Staff)
            .WithMany(s => s.Staffs_HairStyles)
            .HasForeignKey(sh => sh.StaffId);

        builder.Entity<Staff_HairStyle>()
            .HasOne(sh => sh.HairStyle)
            .WithMany(h => h.Staffs_HairStyles)
            .HasForeignKey(sh => sh.HairStyleId);

        builder.Entity<Appointment>()
            .HasOne(a => a.Staff)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.StaffId);

        builder.Entity<Appointment>()
            .HasOne(a => a.HairStyle)
            .WithMany(h => h.Appointments)
            .HasForeignKey(a => a.HairstyleId);

        builder.Entity<HairStyle>()
            .HasOne(h => h.Location)
            .WithMany(l => l.HairStyles)
            .HasForeignKey(h => h.LocationId);

        builder.Entity<Staff>()
            .HasOne(h => h.Location)
            .WithMany(l => l.Staffs)
            .HasForeignKey(h => h.LocationId);

        builder.Entity<Appointment>()
            .HasOne(h => h.Location)
            .WithMany(l => l.Appointments)
            .HasForeignKey(h => h.LocationId);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u =>  u.FirstName).HasMaxLength(255);
        builder.Property(u =>  u.FirstName).HasMaxLength(255);
        builder.Property(u =>  u.ProfileImageUrl).HasMaxLength(255);
    }
}
