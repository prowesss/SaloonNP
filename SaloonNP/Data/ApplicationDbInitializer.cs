using SaloonNP.Data.Enums;
using SaloonNP.Models.ServiceManagementModels;
using SaloonNP.Models.UserManagementModels;

namespace SaloonNP.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (!context.Staff.Any())
                {
                    context.Database.EnsureCreated();
                    var locations = GenerateInitialLocations();
                    var staffs = GenerateInitialStaff();
                    var hairstyles = GenerateInitialHairStyles(staffs, locations);

                    context.Location.Add(locations[0]);
                    context.Location.Add(locations[1]);
                    context.Staff.Add(staffs[0]);
                    context.Staff.Add(staffs[1]);
                    context.Staff.Add(staffs[2]);
                    context.HairStyle.Add(hairstyles[0]);
                    context.HairStyle.Add(hairstyles[1]);
                    context.HairStyle.Add(hairstyles[2]);
                    context.SaveChanges();
                }
            }

        }

        private static List<Staff_HairStyle> GenerateInitialStaff_HairStyle(List<Staff> staffs, List<HairStyle> hairStyles)
        {
            List<Staff_HairStyle> staff_hairstyle = new List<Staff_HairStyle>();
            staff_hairstyle.Add(new Staff_HairStyle()
            {
                StaffId = staffs[0].Id,
                HairStyleId = hairStyles[1].Id
            });
            staff_hairstyle.Add(new Staff_HairStyle()
            {
                StaffId = staffs[1].Id,
                HairStyleId = hairStyles[0].Id
            });

            staff_hairstyle.Add(new Staff_HairStyle()
            {
                StaffId = staffs[2].Id,
                HairStyleId = hairStyles[2].Id
            });
            return staff_hairstyle;
        }

        private static List<HairStyle> GenerateInitialHairStyles(List<Staff> staffs, List<Location> locations)
        {
            List<HairStyle> hairstyles = new List<HairStyle>();
            hairstyles.Add(new HairStyle()
            {
                Id = Guid.NewGuid(),
                Name = "bangs",
                Description = "most popular",
                Gender = Gender.Male,
                ImageURL = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/best-hairstyles-with-bangs-1578064742.png?crop=0.909xw:0.907xh;0.0753xw,0.0769xh&resize=980:*",
                Staffs_HairStyles = new List<Staff_HairStyle>
                {
                    new Staff_HairStyle { Staff = staffs[0] }
                },
                Location = locations[1],
                Price = 56.99
            });
            hairstyles.Add(new HairStyle()
            {
                Id = Guid.NewGuid(),
                Name = "top fade",
                Description = "most popular",
                Gender = Gender.Female,
                ImageURL = "https://styledieter.com/wp-content/uploads/2016/09/1-fade-haircut-with-medium-length-messy-top.jpg",
                Staffs_HairStyles = new List<Staff_HairStyle>
                {
                    new Staff_HairStyle { Staff = staffs[1] }
                },
                Location = locations[0],
                Price = 66.99
            });
            hairstyles.Add(new HairStyle()
            {
                Id = Guid.NewGuid(),
                Name = "mo hawk",
                Description = "most popular",
                Gender = Gender.kids,
                ImageURL = "https://www.menshairstyletrends.com/wp-content/uploads/2021/04/Mohawk-hairstyle-punk-rock-cutzbytommy.jpg",
                Staffs_HairStyles = new List<Staff_HairStyle>
                {
                    new Staff_HairStyle { Staff = staffs[2] }
                },
                Location = locations[1],
                Price = 156.99
            });

            return hairstyles;
        }

        private static List<Staff> GenerateInitialStaff()
        {
            {
                List<Staff> staffs = new List<Staff>();
                staffs.Add(new Staff()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Designer 1",
                    Description = "This is the Bio of the first designer",
                    ProfilePictureURL = "http://dotnethow.net/images/actors/actor-1.jpeg"

                });
                staffs.Add(new Staff()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Designer 2",
                    Description = "This is the Bio of the second designer",
                    ProfilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg"

                });
                staffs.Add(new Staff()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Designer 3",
                    Description = "This is the Bio of the third designer",
                    ProfilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg"

                });

                return staffs;
            }
        }

        private static List<Location> GenerateInitialLocations()
        {
            List<Location> locations = new List<Location>();
            locations.Add(new Location
            {
                Id = Guid.NewGuid(),
                Address = "Henderson",
                City = "Auckland",
                Country = "NewZealand",
                PhoneNumber = "0223388444",
                PostalCode = "0621",
            });
            locations.Add(new Location
            {
                Id = Guid.NewGuid(),
                Address = "lincon Road",
                City = "Auckland",
                Country = "NewZealand",
                PhoneNumber = "0223389444",
                PostalCode = "0623",
            });

            return locations;
        }
    }

}
