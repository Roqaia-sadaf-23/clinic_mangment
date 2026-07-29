

using Clinic_Domain.Common;
using Clinic_Domain.Entities;

namespace Clinic_Domain.Entities;

public class Prescription
{
    public int Id { get;private set; }
    public int MedicalRecordId { get; private set; } = 0;
    public string MedicationName { get;private set; } = null!;

    public string? Frequency { get;private set; }

    public string? Dosage { get; private set; }

    public string? SpecialInstructions { get; private set; }
  
    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
    //  public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();



    private Prescription(int medicalRecordId, string medicationName, string? frequency, string? dosage, string? specialInstructions)
    {
        MedicalRecordId = medicalRecordId;
        MedicationName = medicationName;
        Frequency = frequency;
        Dosage = dosage;
        SpecialInstructions = specialInstructions;

    }

    private Prescription() { }

    static public Prescription Create(int medicalRecordId, string medicationName, string? frequency, string? dosage, string? specialInstructions)
    {
        return new Prescription(medicalRecordId, medicationName, frequency, dosage, specialInstructions);
    }

     public void Update(string medicationName, string? frequency, string? dosage, string? specialInstructions)
    {
        MedicationName = medicationName;
        Frequency = frequency;
        Dosage = dosage;
        SpecialInstructions = specialInstructions;
    }
}
