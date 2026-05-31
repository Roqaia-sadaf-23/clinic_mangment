

using Clinic_Domain.Common;
using Clinic_Domain.Entities;

namespace Clinic_Domain.Entities;

public class Prescription
{
    public int Id { get; set; }

    public string MedicationName { get; set; } = null!;

    public string? Frequency { get; set; }

    public string? Dosage { get; set; }

    public string? SpecialInstructions { get; set; }


    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();



    private Prescription(string medicationName, string? frequency, string? dosage, string? specialInstructions)
    {
        MedicationName = medicationName;
        Frequency = frequency;
        Dosage = dosage;
        SpecialInstructions = specialInstructions;
    }

    private Prescription() { }

    static public Prescription Create(string medicationName, string? frequency, string? dosage, string? specialInstructions)
    {
        return new Prescription(medicationName, frequency, dosage, specialInstructions);

    }

     public void Update(string medicationName, string? frequency, string? dosage, string? specialInstructions)
    {
        MedicationName = medicationName;
        Frequency = frequency;
        Dosage = dosage;
        SpecialInstructions = specialInstructions;
    }
}
