

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                
                new IdentityUserRole<string>
                {
                    RoleId = "7e72af3e-89bb-426c-86a1-0881a69bb65b",
                    UserId = "96c72009-4b64-4c06-ad5b-e278ca2f9e16",

                }
                
           );
        }
    }
}
