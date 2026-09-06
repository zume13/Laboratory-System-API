using Application.Abstractions.Repositories;
using Application.Features.LabOrder.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Queries.GetAllLabOrdersByPatientId
{
    public class GetAllLabOrdersByPatientIdQueryHandler : IRequestHandler<GetAllLabOrdersByPatientIdQuery, ResultT<List<LabOrderDto>>>
    {
        ILabOrderRepository _repository;
        public GetAllLabOrdersByPatientIdQueryHandler(ILabOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultT<List<LabOrderDto>>> Handle(GetAllLabOrdersByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var labOrders = await _repository.GetAllLabOrdersByPatientIdAsync(request.PatientId, cancellationToken);
            
            var labOrderDtos = labOrders.Select(lo => new LabOrderDto(
                lo.Id,
                lo.CreatedAt,
                lo.Status)).ToList();

            return ResultT<List<LabOrderDto>>.Success(labOrderDtos);    
        }
    }
}
