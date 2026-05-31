using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Microsoft.EntityFrameworkCore;
using Clinic_Application.Features.Appointments.Query.GetAvailableSlots;
using Clinic_Domain.Entities.Appointments;
using MediatR;

public class GetAvailableSlotsHandler
    : IRequestHandler<GetAvailableSlotsQuery, List<AvailableSlotDto>>
{
    private readonly IAppDBContext  _context;

    public GetAvailableSlotsHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<List<AvailableSlotDto>> Handle(
        GetAvailableSlotsQuery request,
        CancellationToken cancellationToken)
    {
        var clinicStart = request.Date.Date.AddHours(12);
        var clinicEnd = request.Date.Date.AddHours(22);

        var duration = TimeSpan.FromMinutes(40);

        var bookedSlots = await _context.Appointments
            .Where(a =>
                a.DoctorId == request.DoctorId &&
                a.AppointmentDate.Date == request.Date.Date &&
                a.AppointmentStatus != AppointmentStatus.Cancelled)
            .Select(a => a.AppointmentDate)
            .ToListAsync(cancellationToken);

        var availableSlots = new List<AvailableSlotDto>();

        for (var slot = clinicStart;
             slot.Add(duration) <= clinicEnd;
             slot = slot.Add(duration))
        {
            if (!bookedSlots.Contains(slot))
            {
                availableSlots.Add(new AvailableSlotDto
                {
                    StartTime = slot,
                    EndTime = slot.Add(duration)
                });
            }
        }

        return availableSlots;
    }
}