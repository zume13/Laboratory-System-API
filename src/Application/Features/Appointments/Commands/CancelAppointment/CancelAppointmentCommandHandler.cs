using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using Domain.Services;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CancelAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IAppointmentSlotRepository appointmentSlotRepository,
            IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentSlotRepository = appointmentSlotRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId, cancellationToken);

            if (appointment is null)
                return AppointmentErrors.NotFound;

            var slot = await _appointmentSlotRepository.GetByIdAsync(appointment.AppointmentSlotId, cancellationToken);

            if (slot is null)
                return AppointmentErrors.NotFound;

            var cancelResult = AppointmentBookingService.Cancel(appointment, slot);

            if (cancelResult.IsFailure)
                return cancelResult.Error;

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if(result.IsFailure)
                return result.Error;

            return Result.Success();
        }
    }
}
