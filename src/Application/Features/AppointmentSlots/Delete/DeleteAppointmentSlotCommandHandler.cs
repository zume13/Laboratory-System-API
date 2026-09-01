using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.AppointmentSlot;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Delete
{
    public class DeleteAppointmentSlotCommandHandler : IRequestHandler<DeleteAppointmentSlotCommand, Result>
    {
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAppointmentSlotCommandHandler(
            IAppointmentSlotRepository appointmentSlotRepository,
            IUnitOfWork unitOfWork)
        {
            _appointmentSlotRepository = appointmentSlotRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteAppointmentSlotCommand request, CancellationToken cancellationToken)
        {
            var slot = await _appointmentSlotRepository.GetByIdAsync(request.appointmentSlotId, cancellationToken);
            if (slot is null)
                return AppointmentSlotErrors.NotFound(request.appointmentSlotId);

            if (slot.BookedCount > 0)
                return AppointmentSlotErrors.HasActiveBookings(request.appointmentSlotId);

            _appointmentSlotRepository.Remove(slot);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}