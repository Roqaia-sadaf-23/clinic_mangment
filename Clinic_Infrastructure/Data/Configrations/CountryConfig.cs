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
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            builder.ToTable("Countries");
            builder.HasKey(e => e.Id).HasName("PK__Countrie__3214EC07E2DAF8E3");

            builder.HasIndex(e => e.CountryName, "UQ_CountryName").IsUnique();

            builder.Property(e => e.CountryName).HasMaxLength(100);
        }
    }
    
     
}
