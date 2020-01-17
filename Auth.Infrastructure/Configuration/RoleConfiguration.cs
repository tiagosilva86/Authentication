using Auth.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Code).HasMaxLength(20);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.HasOne(f => f.Application).WithMany(f => f.Roles).HasForeignKey(f => f.Application_Id);
            
        }
    }
}
