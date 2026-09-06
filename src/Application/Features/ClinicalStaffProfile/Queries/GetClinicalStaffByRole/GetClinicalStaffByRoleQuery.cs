using Application.Dto;
using Domain.Aggregates.Identity.UserProfile.Enums;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Queries.GetClinicalStaffByRole
{
    public record GetClinicalStaffByRoleQuery(StaffRole role) : IRequest<ResultT<List<ClinicalStaffProfileDto>>>;
}