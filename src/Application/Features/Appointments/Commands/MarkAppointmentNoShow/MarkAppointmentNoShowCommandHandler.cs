using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.MarkAppointmentNoShow
{
    public class MarkAppointmentNoShowCommandHandler : IRequestHandler<MarkAppointmentNoShowCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MarkAppointmentNoShowCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(MarkAppointmentNoShowCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);

            if (appointment is null)
                return AppointmentErrors.NotFound;

            appointment.MarkNoShow();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
