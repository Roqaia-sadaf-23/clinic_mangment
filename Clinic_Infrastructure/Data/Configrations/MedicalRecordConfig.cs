using Clinic_Domain.Common;
using Clinic_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Infrastructure.Data.Configrations
{
    public class MedicalRecordConfig : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.ToTable("MedicalRecords");

            builder.HasKey(e => e.Id).HasName("PK__MedicalR__3214EC071C1ADD6D");

            builder.Property(e => e.Diagnosis).HasMaxLength(500);
            builder.Property(e => e.PaymentId).HasColumnName("PaymentID");

            builder.Property(e => e.Notes).HasColumnName("Notes");
            builder.Property(e => e.PrescriptionId).HasColumnName("PrescriptionID");
            builder.Property(e => e.VisitDescreption)
                .HasMaxLength(500);

            builder.HasOne(d => d.Appointment).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__Appoi__72910220");

            //builder.HasOne(d => d.Payment).WithMany(p => p.MedicalRecords)
            //    .HasForeignKey(d => d.PaymentId)
            //    .HasConstraintName("FK_MedicalRecords_Payments");

            builder.HasOne(d => d.Prescription).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("FK_MedicalRecords_Prescriptions");
        }
    }
}
