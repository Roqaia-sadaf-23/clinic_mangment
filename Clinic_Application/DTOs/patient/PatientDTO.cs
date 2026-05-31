using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clinic_Application.DTOs.Patient;

public class PatientDTO
{
    public int Id { get; set; }
    public string? BloodType { get; set; } = string.Empty;
    public int PersonId { get; set; }

}
