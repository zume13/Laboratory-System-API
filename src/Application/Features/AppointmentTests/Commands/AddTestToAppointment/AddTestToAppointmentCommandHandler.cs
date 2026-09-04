using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using Domain.Aggregates.Laboratory.TestCategory;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentTests.Commands.AddTestToAppointment
{
    public class AddTestToAppointmentCommandHandler : IRequestHandler<AddTestToAppointmentCommand, ResultT<Guid>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTestToAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            ITestCategoryRepository testCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultT<Guid>> Handle(AddTestToAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.appointmentId, cancellationToken);
            if (appointment is null)
                return AppointmentErrors.NotFound;

            var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId, cancellationToken);
            if (category is null)
                return TestCategoryErrors.NotFound(request.testCategoryId);

            var addResult = appointment.AddTest(request.testCategoryId);
            if (addResult.IsFailure)
                return addResult.Error;

            _appointmentRepository.Update(appointment);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return addResult.value.Id;
        }
    }
}