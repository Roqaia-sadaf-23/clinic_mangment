using Clinic_Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Domain.Entities.Appointments
{
    public static class AppointmentError
    {
        public static Error DoctorNotFound(int doctorId) 
            => Error.Validation("DoctorNotFound", $"Doctor with ID {doctorId} was not found.");
    
    public static Error PatientNotFound(int patientId) 
            => Error.Validation("PatientNotFound", $"Patient with ID {patientId} was not found.");
    }
}
