using Clinic_Domain.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Clinic_Application.DTOs.Appintment
{

    public class ChangeAppointmentStatusDTO
    {
        public AppointmentStatus Status { get; set; }
    }
}