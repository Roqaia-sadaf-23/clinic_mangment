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
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.ID).HasName("PK__People__3214EC0798890DE3");

            builder.Property(e => e.Address).HasMaxLength(100);
            builder.Property(e => e.FirstName).HasMaxLength(100);
            builder.Property(e => e.Gender).HasMaxLength(10);
            builder.Property(e => e.ImagePath).HasMaxLength(250);
            builder.Property(e => e.LastName).HasMaxLength(100);
            builder.Property(e => e.NationalityCountryId).HasColumnName("NationalityCountryID");
            builder.Property(e => e.NationalityNo).HasMaxLength(100);

            //builder.HasOne(d => d.NationalityCountry).WithMany(p => p.People)
            //    .HasForeignKey(d => d.NationalityCountryId)
            //    .HasConstraintName("FK_People_Countries");
        }
    }
}
