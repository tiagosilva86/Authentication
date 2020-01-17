using Auth.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Configuration
{
    public class EndPointConfiguration : IEntityTypeConfiguration<EndPoint>
    {
        public void Configure(EntityTypeBuilder<EndPoint> builder)
        {
            builder.HasKey(k => k.Hash);
            builder.Property(p => p.Description).HasMaxLength(150);
            builder.Property(p => p.Hash).IsRequired();
        }
    }
}
