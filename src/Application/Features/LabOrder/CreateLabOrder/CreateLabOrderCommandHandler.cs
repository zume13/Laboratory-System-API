using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using Domain.Services;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.CreateLabOrder
{
    public class CreateLabOrderCommandHandler : IRequestHandler<CreateLabOrderCommand, ResultT<Guid>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILabOrderRepository _labOrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateLabOrderCommandHandler(IAppointmentRepository appointmentRepository, ILabOrderRepository labOrderRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _labOrderRepository = labOrderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultT<Guid>> Handle(CreateLabOrderCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithAppointmentTestAsync(request.AppointmentId);

            if (appointment is null)
                return AppointmentErrors.NotFound;

            var labOrder = AppointmentFulfillmentService.Fulfill(appointment);

            if(labOrder.IsFailure)
                return labOrder.Error;

            await _labOrderRepository.AddAsync(labOrder.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if(saveResult.IsFailure)
                return saveResult.Error;

            return ResultT<Guid>.Success(appointment.Id);
        }
    }
}
