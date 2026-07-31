using Clinic_Domain.Common;
using Clinic_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Clinic_Infrastructure.Data.Configrations
{
    public class MedicalRecordConfig : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.ToTable("MedicalRecords");

            builder.HasKey(e => e.Id)
                .HasName("PK__MedicalR__3214EC071C1ADD6D");

            builder.Property(e => e.Diagnosis)
                .HasMaxLength(500);

            builder.Property(e => e.Notes)
                .HasColumnName("Notes");

            builder.Property(e => e.VisitDescreption)
                .HasMaxLength(500);

            builder.HasIndex(e => e.AppointmentId)
                .IsUnique()
                .HasDatabaseName(
                    "UX_MedicalRecords_AppointmentId");

            builder.HasOne(d => d.Appointment)
                .WithOne(p => p.MedicalRecord)
                .HasForeignKey<MedicalRecord>(
                    d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK__MedicalRe__Appoi__72910220"); ;
        }
    }
}
