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
    public class PrescriptionConfig : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {

            builder.HasKey(e => e.Id).HasName("PK__Prescrip__3214EC07734BBE85");

            builder.Property(e => e.Dosage).HasMaxLength(100);
            builder.Property(e => e.Frequency).HasMaxLength(50);
            builder.Property(e => e.MedicationName).HasMaxLength(200);
            builder.Property(e => e.SpecialInstructions).HasMaxLength(500);

        }
    }
}
