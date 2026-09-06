using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Queries.GetStaffProfile
{
    public record GetStaffProfileQuery(Guid userId) : IRequest<ResultT<ClinicalStaffProfileDto>>;
}