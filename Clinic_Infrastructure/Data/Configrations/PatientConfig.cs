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
  
   public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.BloodType).HasMaxLength(10);
            builder.Property(e => e.PersonId).HasColumnName("PersonID");

            builder.HasOne(p => p.Person)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(p => p.PersonId)
                .HasPrincipalKey<Person>(p => p.ID)
                .OnDelete(DeleteBehavior.Cascade);

            //PersonId is uniqe in  DataBase
        }
    }
}