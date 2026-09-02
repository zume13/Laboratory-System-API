using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using Domain.Aggregates.Laboratory.TestCategory;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.ChangeAppointmentTestCategory
{
    public class ChangeAppointmentTestCategoryCommandHandler : IRequestHandler<ChangeAppointmentTestCategoryCommand, Result>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeAppointmentTestCategoryCommandHandler(IAppointmentRepository appointmentRepository, ITestCategoryRepository testCategoryRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(ChangeAppointmentTestCategoryCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);

            if (appointment == null)
                return AppointmentErrors.NotFound;

            var newTestCategory = await _testCategoryRepository.GetByIdAsync(request.NewTestCategoryId);

            if (newTestCategory == null)
                return TestCategoryErrors.NotFound(request.NewTestCategoryId);

            appointment.ChangeTestCategory(newTestCategory.Id);    

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();    
        } 
    }
}
