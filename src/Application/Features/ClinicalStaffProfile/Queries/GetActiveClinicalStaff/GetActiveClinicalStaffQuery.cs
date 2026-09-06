using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Queries.GetActiveClinicalStaff
{
    public record GetActiveClinicalStaffQuery() : IRequest<ResultT<List<ClinicalStaffProfileDto>>>;
}