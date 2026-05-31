
using Clinic_Domain.Entities;
using Clinic_Domain.Entities.Appointments;
using Microsoft.EntityFrameworkCore;


namespace Clinic_Application.Common.Interfaces;

    public interface IAppDBContext
    {

    public  DbSet<Appointment> Appointments { get;   }

    public   DbSet<Country> Countries { get;   }

    public   DbSet<Doctor> Doctors { get;   }

    public   DbSet<MedicalRecord> MedicalRecords { get;   }

    public   DbSet<Patient> Patients { get;   }

    public   DbSet<Payment> Payments { get;   }

    public   DbSet<Person> People { get;   }

    public   DbSet<Prescription> Prescriptions { get;   }

    public   DbSet<RefreshToken> RefreshTokens { get;   }

    public   DbSet<Role> Roles { get;   }

    public   DbSet<User> Users { get;   }

    public   DbSet<UserRole> UserRoles { get;   }



    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

