using Application.Features.LabOrder.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Queries.GetAllLaboratoryRequestById
{
    public record GetAllLaboratoryRequestByIdQuery(Guid patientId) : IRequest<ResultT<List<LabOrderDto>>>;
}