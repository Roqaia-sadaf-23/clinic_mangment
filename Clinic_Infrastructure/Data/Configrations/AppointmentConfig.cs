using Clinic_Domain.Entities.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Infrastructure.Data.Configrations
{
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Appointments");

            builder.Property(e => e.AppointmentDate)
                .HasColumnType("datetime");

            builder.Property(e => e.CreatedByUserId);

            builder.Property(e => e.DurationInMinutes)
                .HasDefaultValue(40);

            builder.Property(e => e.LastModifiedBy)
                .HasMaxLength(100);

            builder.Property(e => e.LastStatusDate)
                .HasColumnType("datetime");

            builder.Property(e => e.MedicalRecordId)
                .HasColumnName("MedicalRecordID");

            builder.Property(e => e.Notes)
                .HasMaxLength(500);

            builder.Property(e => e.AppointmentStatus)
                .HasColumnName("Status")
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(AppointmentStatus.Pending);

            builder.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(a => a.StatusText);
        }
    }
}