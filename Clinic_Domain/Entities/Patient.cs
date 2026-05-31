using Clinic_Domain.Entities.Appointments;
using Clinic_Domain.Entities;

namespace Clinic_Domain.Entities;


public class Patient
{
     public int Id { get; private set; }
    public string? BloodType { get; private set; }
    public int PersonId { get; private set; }
   public Person Person { get; private set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();


    //  public List<Appointment> Appointments { get; private set; } = new();

    private Patient() { } // EF Core

    private Patient(string? bloodType, int PersonID)
    {
        BloodType = bloodType;
        PersonId = PersonID;
    }
    public static Patient Create(string? bloodType, int PersonID)
    {
        return new Patient(bloodType, PersonID);
    }

    public void UpdateBloodType(string bloodType)
    {
        BloodType = bloodType;
    }

    public void AddAppointment(Appointment appointment)
    {
        if (appointment.PatientId != this.Id)
            throw new InvalidOperationException
                ("Appointment does not belong to this patient.");

        Appointments.Add(appointment);
    }

    public void CancelAppointment(Appointment appointment)
    {
        if (!Appointments.Contains(appointment))
            throw new InvalidOperationException("Appointment not found for this patient.");

        appointment.Cancel();
    }

    public void Update(string bloodType, int PersonID)
    {
        BloodType = bloodType;
        PersonID = PersonID;
    }
}