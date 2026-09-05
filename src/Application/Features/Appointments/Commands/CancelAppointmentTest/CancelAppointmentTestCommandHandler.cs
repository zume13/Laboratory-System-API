using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.CancelAppointmentTest
{
    public class CancelAppointmentTestCommandHandler : IRequestHandler<CancelAppointmentTestCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelAppointmentTestCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(CancelAppointmentTestCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithAppointmentTestAsync(request.appointmentId, cancellationToken);

            if (appointment == null)
                return AppointmentErrors.NotFound;

            var appointmentTest = appointment.Tests.FirstOrDefault(t => t.Id == request.appointmentTestId);

            if(appointmentTest is null)
                return AppointmentErrors.AppointmentTestNotFound;   

            var rejecteResult = appointment.RejectAppointmentTest(appointmentTest.Id);
            
            if(rejecteResult.IsFailure)
                return rejecteResult;

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}
