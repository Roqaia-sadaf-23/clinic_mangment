using Clinic_Domain.Entities.Appointments;
using Clinic_Domain.Enums;

namespace Clinic_Domain.Entities;


public class Payment
{
    public int Id { get; private set; }

    public int AppointmentId { get; private set; }
    public Appointment Appointment { get; private set; }
   
    public int PatientId { get; private set; }
    public Patient Patient { get; private set; }
    public decimal Amount { get; private set; }

    public PaymentStatus Status { get; private set; }

    public string? PaymentMethod { get; private set; }

    public string? Note { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Payment() { }

    private Payment(
        int appointmentId,
        int patientId,
        string? paymentMethod,
        decimal amount,
        string? note)
    {
        AppointmentId = appointmentId;
        PatientId = patientId;
        Amount = amount;
        Note = note;
        PaymentMethod = paymentMethod;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.Now;
    }

    public static Payment Create(
        int appointmentId,
        int patientId,
        string? paymentMethod,
        decimal amount,
        string? note)
    {
        if (appointmentId <= 0)
            throw new ArgumentException("Invalid appointment.");

        if (patientId <= 0)
            throw new ArgumentException("Invalid patient.");

        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        return new Payment(
            appointmentId,
            patientId,
            paymentMethod,
            amount,
            note);
    }

    public void UpdatePayment(
        decimal amount,
        string? paymentMethod,
        string? note)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");
        Amount = amount;
        PaymentMethod = paymentMethod;
        Note = note;
    }

    public void MarkAsPaid(string paymentMethod)
    {
        Status = PaymentStatus.Paid;
        PaymentMethod = paymentMethod;
    }

    public void Cancel()
    {
        Status = PaymentStatus.Cancelled;
    }
}