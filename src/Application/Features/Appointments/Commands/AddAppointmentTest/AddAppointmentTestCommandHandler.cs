using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.AddAppointmentTest
{
    public class AddAppointmentTestCommandHandler : IRequestHandler<AddAppointmentTestCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddAppointmentTestCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(AddAppointmentTestCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithAppointmentTestAsync(request.appointmentId, cancellationToken);

            if(appointment is null)
                return AppointmentErrors.NotFound;

            var addTestResult = appointment.AddTest(request.testCategoryId);

            if (addTestResult.IsFailure)
                return addTestResult.Error;

            var saveChangesResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveChangesResult.IsFailure)
                return saveChangesResult.Error;

            return Result.Success();
        }
    }
}
