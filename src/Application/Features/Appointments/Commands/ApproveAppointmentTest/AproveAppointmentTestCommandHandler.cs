using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.ApproveAppointmentTest
{
    public class AproveAppointmentTestCommandHandler : IRequestHandler<AproveAppointmentTestCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AproveAppointmentTestCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(AproveAppointmentTestCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithAppointmentTestAsync(request.appointmentId, cancellationToken);

            if (appointment is null)
                return AppointmentErrors.NotFound;

            var appointmentTest = appointment.Tests.FirstOrDefault(t => t.Id == request.appointmentTestId);

            if (appointmentTest is null)
                return AppointmentErrors.AppointmentTestNotFound;

            var approveTestResult = appointment.ApproveAppointmentTest(appointmentTest.Id);    

            if (approveTestResult.IsFailure)
                return approveTestResult.Error;

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();    
        }
    }
}