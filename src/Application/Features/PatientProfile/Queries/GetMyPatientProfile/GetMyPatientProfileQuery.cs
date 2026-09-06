using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Queries.GetMyPatientProfile
{
    public record GetMyPatientProfileQuery(Guid userId) : IRequest<ResultT<PatientProfileDto>>;
}