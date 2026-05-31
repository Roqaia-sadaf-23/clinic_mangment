
using Clinic_Domain.Entities.Appointments;
 
namespace Clinic_Domain.Entities;

public class MedicalRecord
{
     public int Id { get; private set; }
    public string Diagnosis { get; private set; } = string.Empty;
    public string? Notes { get; private set; }
    public string? VisitDescreption { get; private set; }

    public int AppointmentId { get; private set; }
    public Appointment Appointment { get; private set; } = null!;

    public int? PaymentId { get; private set; }
    public Payment? Payment { get; private set; } = null!;

    public int? PrescriptionId { get; private set; }
    public Prescription? Prescription { get; private set; }

    private MedicalRecord() { }

    private MedicalRecord(
        string diagnosis,
        string? notes,
        string? visitDescription,
        int appointmentId)
    {
        Diagnosis = diagnosis;
        Notes = notes;
        VisitDescreption = visitDescription;
        AppointmentId = appointmentId;
    }

    public static MedicalRecord Create(
        string diagnosis,
        string? notes,
        string? visitDescription,
        int appointmentId, int? paymentId, int? prescriptionId)
    {
        if (string.IsNullOrWhiteSpace(diagnosis))
            throw new ArgumentException("Diagnosis is required.");

        if (appointmentId <= 0)
            throw new ArgumentException("AppointmentId must be greater than 0.");

        return new MedicalRecord(diagnosis, notes, visitDescription, appointmentId);
    }

    public void UpdateDiagnosis(string diagnosis,
        string? notes,
        string? visitDescription,
        int appointmentId, int? paymentId, int? prescriptionId)
    {
        if (string.IsNullOrWhiteSpace(diagnosis))
            throw new ArgumentException("Diagnosis is required.");
        if(appointmentId <= 0)
            throw new ArgumentException("AppointmentId must be greater than 0.");
        Diagnosis = diagnosis;
        Notes = notes;
        VisitDescreption = visitDescription;
        AppointmentId = appointmentId;
        PaymentId = paymentId;
        PrescriptionId = prescriptionId;
    }

    public void UpdateNotes(string? notes)
    {
        Notes = notes;
    }

    public void UpdateVisitDescription(string? visitDescription)
    {
        VisitDescreption = visitDescription;
    }

    public void AttachPayment(int paymentId)
    {
        if (paymentId <= 0)
            throw new ArgumentException("PaymentId must be greater than 0.");

        PaymentId = paymentId;
    }

    public void AttachPrescription(int prescriptionId)
    {
        if (prescriptionId <= 0)
            throw new ArgumentException("PrescriptionId must be greater than 0.");

        PrescriptionId = prescriptionId;
    }
}