using Clinic_Domain.Common;
using Clinic_Domain.Entities;
using Clinic_Domain.Entities.Appointments;



namespace Clinic_Domain.Entities.Appointments;

public partial class Appointment
{
    public int Id { get; set; }

    public int DoctorId { get;private set; }
    public Doctor Doctor { get;private set; }
    public int PatientId { get; private set; }
    public Patient Patient { get; private set; }
    public DateTime AppointmentDate { get; private set; }

    // public int? Status { get; set; }
    public AppointmentStatus AppointmentStatus { get; private set; }
        = AppointmentStatus.Pending;

    public string StatusText
    {
        get
        {
            switch (AppointmentStatus)
            {
                case AppointmentStatus.Pending:
                    return "Pending";
                case AppointmentStatus.Confirmed:
                    return "Confirmed";
                case AppointmentStatus.Cancelled:
                    return "Cancelled";
                case AppointmentStatus.Completed:
                    return "Completed";
                default:
                    return "Unknown";
            };
        }



    }
  
    
    public DateTime LastStatusDate { get; private set; }

    public virtual MedicalRecord? MedicalRecord { get; set; }
    public virtual Payment? Payment { get; set; }
    public DateTimeOffset? CreatedAt { get; private set; }

    public int? CreatedByUserId { get; private set; }
 //   public User User { get; set; }
    public DateTimeOffset? LastModifiedAt { get; private set; }

    public string? LastModifiedBy { get; private set; }

    public int DurationInMinutes { get; private set; } = 40;



    private Appointment() { }
   
    private Appointment(int doctorId, int patientId, DateTime appointmentDate, int? userId)
    {
        DoctorId = doctorId;
        PatientId = patientId;
        AppointmentDate = appointmentDate;
      //  DurationInMinutes = durationInMinutes;
      //  Notes = notes;
        AppointmentStatus = AppointmentStatus.Pending;
        CreatedByUserId = userId;
        LastStatusDate = DateTime.Now;
        CreatedAt = DateTime.Now;
    }

    public static Appointment Create(int doctorId, int patientId, DateTime appointmentDate, int? userId)
    {
        if (doctorId <= 0)
            throw new ArgumentException("DoctorId must be greater than 0.");

        if (patientId <= 0)
            throw new ArgumentException("PatientId must be greater than 0.");

        if (appointmentDate < DateTime.Now)
            throw new ArgumentException("Appointment date cannot be in the past.");

        return new Appointment(doctorId, patientId, appointmentDate,userId);
    }

    //public void Update(int doctorId, int patientId, DateTime appointmentDate, string? notes)
    //{
    //    DoctorId = doctorId;
    //    PatientId = patientId;
    //    if (appointmentDate < DateTime.Now)
    //        throw new ArgumentException("Appointment date cannot be in the past.");
    //    Notes = notes;

    //}
    public void Reschedule(DateTime newDate)
    {
        if (newDate < DateTime.Now)
            throw new ArgumentException("Appointment date cannot be in the past.");

        AppointmentDate = newDate;
    }

    public void Cancel()
    {
        AppointmentStatus = AppointmentStatus.Cancelled;
        LastStatusDate = DateTime.Now;
    }

    public void Complete()
    {
        AppointmentStatus = AppointmentStatus.Completed;
        LastStatusDate = DateTime.Now;
    }

    //public void UpdateNotes(string? notes)
    //{
    //    Notes = notes;
    //}

    //public void AttachMedicalRecord(int medicalRecordId)
    //{
    //    if (medicalRecordId <= 0)
    //        throw new ArgumentException("MedicalRecordId must be greater than 0.");

    //    MedicalRecordId = medicalRecordId;
    //}
}
