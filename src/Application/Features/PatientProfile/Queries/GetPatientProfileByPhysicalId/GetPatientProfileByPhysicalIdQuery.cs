using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Queries.GetPatientProfileByPhysicalId
{
    public record GetPatientProfileByPhysicalIdQuery(string physicalPatientId) : IRequest<ResultT<PatientProfileDto>>;
}