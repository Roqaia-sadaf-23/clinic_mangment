 using Clinic_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace Clinic_Infrastructure.Data.Configrations
{
    public class DoctorConfig :IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {

            builder.ToTable("Doctors");
            builder.HasKey(e => e.Id).HasName("PK__Doctors__3214EC07375E126F");

            builder.Property(e => e.PersonId).HasColumnType("int")
                .HasColumnName("PersonID");
            builder.Property(e => e.Specialty).HasMaxLength(100);

            builder.HasOne(d => d.Person).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Doctors__PersonI__719CDDE7");
            //PersonId is uniqe in  DataBase
            builder.Property(e => e.ExperienceYears)
    .HasColumnType("int").HasColumnName("ExperienceYears")
    .HasDefaultValue(0);
        
        }
    }
    
   
}
