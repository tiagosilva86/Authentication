using Auth.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Configuration
{
    class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //TWO COLUMN FORMING PRIMARY KEY
            builder.HasKey(k => new {
                k.User_Id,
                k.Role_Id
            });
            //AS NET CORE NOW REQUIRES MANY TO MANY TO HAVE A CLASS DOING THE LINK BETWEEN THEM
            // THE RELATION NOW MUST BE DECLARED LIKE BELOW
            //ROLECLAIM TABLE HAS ONE MANY CLAIMS AND MANY ROLES FORMING ONE TO ONE.
            //E.g. KEY 1 - KEY 1, KEY 1 - KEY 2, KEY 2 - KEY 2.....
            builder.HasOne(fk => fk.User)
                .WithMany(fk => fk.Roles)
                .HasForeignKey(fk => fk.User_Id);
            builder.HasOne(fk => fk.Role)
                .WithMany()
                .HasForeignKey(fk => fk.Role_Id);
        }
    }
}
