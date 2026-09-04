using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.RemoveAppointmentTest
{
    public class RemoveAppointmentTestCommandHandler : IRequestHandler<RemoveAppointmentTestCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveAppointmentTestCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(RemoveAppointmentTestCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithAppointmentTestAsync(request.appointmentId, cancellationToken);

            if (appointment is null)
                return AppointmentErrors.NotFound;

            var removeTestResult = appointment.RemoveTest(request.testCategoryId);

            if (removeTestResult.IsFailure)
                return removeTestResult.Error;

            var saveChangesResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveChangesResult.IsFailure)
                return saveChangesResult.Error;

            return Result.Success();
        }
    }
}
