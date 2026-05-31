using Clinic_Domain.Entities;
using Clinic_Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


    namespace Clinic_Infrastructure.Data.Configrations
    {
        public class PaymentConfig : IEntityTypeConfiguration<Payment>
        {
            public void Configure(EntityTypeBuilder<Payment> builder)
            {
                builder.ToTable("Payments");

                builder.HasKey(p => p.Id);

                builder.Property(p => p.Amount)
                    .HasColumnType("decimal(18,2)");

                builder.Property(p => p.PaymentMethod)
                    .HasMaxLength(50);

                builder.Property(p => p.Note)
                    .HasMaxLength(500);

                builder.Property(p => p.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20).HasDefaultValue(PaymentStatus.Pending);

                builder.HasOne(p => p.Appointment)
                    .WithMany()
                    .HasForeignKey(p => p.AppointmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(p => p.Patient)
                    .WithMany()
                    .HasForeignKey(p => p.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
