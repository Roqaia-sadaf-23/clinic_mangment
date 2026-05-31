using Clinic_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Infrastructure.Data.Configrations
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC07A3807403");

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.ExpiresAt).HasColumnType("datetime");
            builder.Property(e => e.RevokedAt).HasColumnType("datetime");
            builder.Property(e => e.IsUsed).HasDefaultValue(false);
            builder.Property(e => e.Revoked).HasDefaultValue(false);
            builder.Property(e => e.Token).HasMaxLength(500);

            builder.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefreshTokens_Users");
        }
    }
}