using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment.Enums;
using Domain.Aggregates.AppointmentSlot;
using Domain.Services;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.CreateOnlineAppointment
{
    public class CreateOnlineAppointmentCommandHandler : IRequestHandler<CreateOnlineAppointmentCommand, ResultT<Guid>>
    {
        private readonly IAppointmentSlotRepository _slotRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOnlineAppointmentCommandHandler(IAppointmentSlotRepository slotRepository, IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _slotRepository = slotRepository;
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ResultT<Guid>> Handle(CreateOnlineAppointmentCommand request, CancellationToken cancellationToken)
        {
            var slot = await _slotRepository.GetByIdAsync(request.appointmentSlotId, cancellationToken);

            if (slot == null)
                return AppointmentSlotErrors.NotFound(request.appointmentSlotId);

            var bookSlot = AppointmentBookingService.Book(slot, request.patientId, request.testCategoryId, BookingChannel.Online);

            if(bookSlot.IsFailure)
                return bookSlot.Error;

            await _appointmentRepository.AddAsync(bookSlot.value, cancellationToken);
            
            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)   
                return saveResult.Error;

            return ResultT<Guid>.Success(bookSlot.value.Id);
        }
    }
}
