using Auth.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Configuration
{
    class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.Property(p => p.IssuedBy).HasMaxLength(30);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");
            builder.HasOne(f => f.Application).WithMany().HasForeignKey(f=>f.Application_Id).IsRequired();
            builder.HasOne(f => f.User).WithMany().HasForeignKey(f => f.User_Id).IsRequired(true);

        }
    }
}
