using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Application.Features.LabOrder.Dto;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Queries.GetLabOrderByPatientId
{
    public class GetPatientLabOrderQueryHandler : IRequestHandler<GetPatientLabOrderQuery, ResultT<LabOrderWithRequestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILabOrderRepository _labOrderRepository;
        public GetPatientLabOrderQueryHandler(IUnitOfWork unitOfWork, ILabOrderRepository labOrderRepository)
        {
            _unitOfWork = unitOfWork;
            _labOrderRepository = labOrderRepository;
        }

        public async Task<ResultT<LabOrderWithRequestDto>> Handle(GetPatientLabOrderQuery request, CancellationToken cancellationToken)
        {
            var labOrder = await _labOrderRepository.GetLabOrderWithLabRequestAsync(request.labOrderId, cancellationToken);

            if (labOrder is null)
                return LaboratoryOrderErrors.LabOrder.NotFound;

            return ResultT<LabOrderWithRequestDto>.Success(labOrder);
        }
    }
}
