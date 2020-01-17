using Auth.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Configuration
{
   public class RoleEndPointConfiguration : IEntityTypeConfiguration<RoleEndPoint>
    {
        public void Configure(EntityTypeBuilder<RoleEndPoint> builder)
    {
            builder.HasKey(k => new {
                k.EndPoint_Id,
                k.Role_Id
            });

            builder.HasOne(fk => fk.EndPoint)
                .WithMany(fk=> fk.Roles)
                .HasForeignKey(fk => fk.EndPoint_Id);
            builder.HasOne(fk => fk.Role)
                .WithMany(fk=> fk.EndPoints)
                .HasForeignKey(fk => fk.Role_Id);
        }
}
}
