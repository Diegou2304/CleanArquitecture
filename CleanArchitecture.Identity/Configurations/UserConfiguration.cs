using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "96c72009-4b64-4c06-ad5b-e278ca2f9e16",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Name = "Diego",
                    Lastnames = "Uribe Blatnik",
                    NormalizedUserName = "diegouribe",
                    PasswordHash = hasher.HashPassword(null, "Diego230498"),
                    EmailConfirmed = true,
                    UserName = "Blackdog2304"
                }

           );
        }
    }
}
    