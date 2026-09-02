using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment.Enums;
using Domain.Aggregates.AppointmentSlot;
using Domain.Services;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.CreateWalkInAppointment
{
    public class CreateWalkInAppointmentCommandHandler : IRequestHandler<CreateWalkInAppointmentCommand, ResultT<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        public CreateWalkInAppointmentCommandHandler(
            IUnitOfWork unitOfWork,
            IAppointmentRepository appointmentRepository,
            IAppointmentSlotRepository appointmentSlotRepository)
        {
            _unitOfWork = unitOfWork;
            _appointmentRepository = appointmentRepository;
            _appointmentSlotRepository = appointmentSlotRepository;
        }
        public async Task<ResultT<Guid>> Handle(CreateWalkInAppointmentCommand request, CancellationToken cancellationToken)
        {
            var slot = await _appointmentSlotRepository.GetByIdAsync(request.appointmentSlotId, cancellationToken);

            if (slot == null)
                return AppointmentSlotErrors.NotFound(request.appointmentSlotId);

            var bookSlot = AppointmentBookingService.Book(
                slot,
                request.patientId,
                request.testCategoryId, 
                BookingChannel.WalkIn);

            if(bookSlot.IsFailure)
                return bookSlot.Error;   

            await _appointmentRepository.AddAsync(bookSlot.value, cancellationToken);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result.IsFailure)
                return result.Error;

            return ResultT<Guid>.Success(bookSlot.value.Id);
        }
    }
}
