using Clinic_Domain.Common;
using Clinic_Domain.Entities.Appointments;


namespace Clinic_Domain.Entities;

public partial class Doctor 
{
    public int Id { get; private set; }
   // public int Id { get; set; }


    public string Specialty { get; private set; } = null!;

    public DateTime? HireDate { get; private set; }
  
    public int PersonId { get; private set; }
    public int? ExperienceYears { get; private set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();


    public virtual Person Person { get; set; } = null!;
    private Doctor() { }

    private Doctor(string specialty, DateTime? hireDate, int Personid,int? experienceYears)

    { 
            Specialty = specialty;
            HireDate = hireDate;
            PersonId = Personid;
        ExperienceYears = experienceYears;
        }

       static public  Doctor Create(string specialty, DateTime? hireDate,
            int PersonId,int? experienceYears)
    {
        if (string.IsNullOrWhiteSpace(specialty))
            throw new ArgumentException("Specialty is required.");

        if (PersonId <= 0)
            throw new ArgumentException("PersonId must be greater than 0.");

        if (hireDate > DateTime.UtcNow)
            throw new ArgumentException("HireDate cannot be in the future.");

        return new Doctor(specialty, hireDate, PersonId, experienceYears);
    }



    public void UpdateHireDate(DateTime newHireDate)
    {
        if (newHireDate > DateTime.Now)
            throw new ArgumentException("HireDate cannot be in the future.");
        HireDate = newHireDate;
    }


    public void ChangeSpecialty(string specialty)
    {
        Specialty = specialty;
    }

    public void Update(string specialty, int Personid,int? experienceYears)
    {

        if (string.IsNullOrWhiteSpace(specialty))
            throw new ArgumentException("Specialty is required.");
        if (PersonId <= 0)
            throw new ArgumentException("PersonId must be greater than 0.");


        Specialty = specialty;
        PersonId = Personid;
        ExperienceYears = experienceYears;
    }
}