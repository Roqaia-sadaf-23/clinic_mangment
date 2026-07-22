

namespace Clinic_Application.DTOs.Appintment
{
    public class AppointmentSummaryDTO
    {

        public int TodayAppointments { get; set; } = 0;
        public int PendingAppointments { get; set; } = 0;
        public int CompletedAppointments { get; set; } = 0;
        public int CancelledAppointments { get; set; } = 0;


}
}
