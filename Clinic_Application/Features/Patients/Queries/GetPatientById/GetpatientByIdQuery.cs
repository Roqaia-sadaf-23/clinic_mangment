using Clinic_Application.DTOs.patient;
using MediatR;


namespace Clinic_Application.Features.Patients.Queries.GetPatientById
{
    public  sealed record GetPatientByIdQuery(int id) : IRequest<PatientInfoDto>;
    
}
