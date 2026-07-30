 using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.MedicalRecord;
using Clinic_Application.DTOs.Payment;
using Clinic_Application.Features.MedicalRecord.Query.GetDoctorPatientMedicalRecord;
using Clinic_Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Clinic_Application.Features.Payment.Query.GetDoctorPatientPayments
{
    public sealed class GetDoctorPatientPaymentsQueryHandler(IAppDBContext context) : IRequestHandler<GetDoctorPatientPaymentsQuery, List<DoctorPatientPaymentDTO>>
    {
        public async Task<List<DoctorPatientPaymentDTO>> Handle(GetDoctorPatientPaymentsQuery request, CancellationToken cancellationToken)
        {


            var personId = await context.Users
    .AsNoTracking()
    .Where(u => u.Id == request.UserId)
    .Select(u => (int?)u.PersonId)
    .FirstOrDefaultAsync(cancellationToken);

            if (personId is null)
                throw new Exception("User not found.");

            var doctorId = await context.Doctors
                .AsNoTracking()
                .Where(d => d.PersonId == personId.Value)
                .Select(d => (int?)d.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (doctorId is null)
                throw new Exception("Doctor not found.");

            var patientBelongsToDoctor = await context.Appointments
                .AsNoTracking()
                .AnyAsync(
                    a => a.DoctorId == doctorId.Value &&
                         a.PatientId == request.PatientId,
                    cancellationToken);

            if (!patientBelongsToDoctor)
                throw new UnauthorizedAccessException(
                    "This patient does not belong to the current doctor.");



            var payments = await context.Payments.AsNoTracking()
                .Where(p => p.Appointment.DoctorId == doctorId.Value &&
                            p.Appointment.PatientId == request.PatientId).
                            OrderByDescending(p=>p.Appointment.AppointmentDate)
                .Select(p => new DoctorPatientPaymentDTO
                {
                    PaymentId = p.Id,
                    AppointmentId = p.AppointmentId,
                    AppointmentDate = p.Appointment.AppointmentDate,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                
                    CreatedAt = p.CreatedAt,
                    Note = p.Note
                })
                .ToListAsync(cancellationToken); return payments;   
        }
    } }