
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(

                new IdentityRole
                {
                    Id = "7e72af3e-89bb-426c-86a1-0881a69bb65b",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                },
                 new IdentityRole
                 {
                     Id = "e0fa01c0-9f95-4004-beb5-afb3c4234edc",
                     Name = "Operator",
                     NormalizedName = "OPERATOR",
                 }

           );
        }
    }
}
