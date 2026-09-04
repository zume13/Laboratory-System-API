using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentTests.Commands.RemoveTestFromAppointment
{
    public class RemoveTestFromAppointmentCommandHandler : IRequestHandler<RemoveTestFromAppointmentCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTestFromAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveTestFromAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.appointmentId, cancellationToken);
            if (appointment is null)
                return AppointmentErrors.NotFound;

            var removeResult = appointment.RemoveTest(request.appointmentTestId);
            if (removeResult.IsFailure)
                return removeResult.Error;

            _appointmentRepository.Update(appointment);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}