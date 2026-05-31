using Clinic_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Clinic_Infrastructure.Data.Configrations
{
    public class RoleConfig : IEntityTypeConfiguration<Role>    
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Roles__3214EC07F8A3E322");

            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.RoleName).HasMaxLength(50);
        }
    }
}
