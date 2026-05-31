using Clinic_Domain.Common;
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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
          //  builder.HasKey(e => e.Id).HasName("PK__Users__3214EC07B8D105DF");

            builder.HasIndex(e => e.Email, "UQ__Users__A9D1053451C9402F").IsUnique();
            builder.HasIndex(e => e.PersonId, "UQ__Users__AA2FFBE4C6FFC198").IsUnique();

            builder.HasIndex(e => e.UserName, "UQ__Users__C9F284566A333931").IsUnique();

            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Email).HasMaxLength(255);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.PasswordHash).HasMaxLength(500);
            builder.Property(e => e.UserName).HasMaxLength(100);
            builder.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Users_People");


            //PersonId is uniqe in  DataBase


        }
    }
}
