using Application.Abstractions.Repositories;
using Application.Features.LabOrder.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Queries.GetAllLaboratoryRequestById
{
    public class GetAllLaboratoryRequestByIdQueryHandler : IRequestHandler<GetAllLaboratoryRequestByIdQuery, ResultT<List<LabOrderDto>>>
    {
        private readonly ILabOrderRepository _labOrderRepository;

        public GetAllLaboratoryRequestByIdQueryHandler(ILabOrderRepository labOrderRepository)
        {
            _labOrderRepository = labOrderRepository;
        }

        public async Task<ResultT<List<LabOrderDto>>> Handle(GetAllLaboratoryRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _labOrderRepository.GetAllLabOrdersByPatientIdAsync(request.patientId, cancellationToken);

            return orders.Select(o => new LabOrderDto(o.Id, o.CreatedAt, o.Status)).ToList();
        }
    }
}