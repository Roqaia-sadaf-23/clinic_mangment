using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clinic_Application.DTOs.Patient;

public class CreatePatientDTO
{
    public string? BloodType { get; set; } 
    public int PersonId { get; set; }
}
