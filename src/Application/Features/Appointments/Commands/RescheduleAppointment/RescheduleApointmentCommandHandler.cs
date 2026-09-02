using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using Domain.Aggregates.AppointmentSlot;
using Domain.Services;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.RescheduleAppointment   
{
    public class RescheduleApointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RescheduleApointmentCommandHandler(
            IAppointmentRepository appointmentRepository, 
            IAppointmentSlotRepository appointmentSlotRepository, 
            IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentSlotRepository = appointmentSlotRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);

            if (appointment is null)
                return AppointmentErrors.NotFound;

            var currentAppointmentSlot = await _appointmentSlotRepository.GetByIdAsync(request.CurrentAppointmentSlotId);

            if (currentAppointmentSlot is null)
                return AppointmentSlotErrors.NotFound(request.CurrentAppointmentSlotId);

            var newAppointmentSlot = await _appointmentSlotRepository.GetByIdAsync(request.NewAppointmentSlotId);

            if (newAppointmentSlot is null)
                return AppointmentSlotErrors.NotFound(request.NewAppointmentSlotId);

            var updateResult = AppointmentBookingService.UpdateSlot(appointment, currentAppointmentSlot, newAppointmentSlot);

            if (updateResult.IsFailure)
                return updateResult;

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if(result.IsFailure)
                return result.Error;

            return Result.Success();
        }   
    }
}
