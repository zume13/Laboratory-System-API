using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.PatientProfile;
using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;
using Domain.Aggregates.Laboratory.TestCategory;
using MediatR;
using SharedKernel.Shared;
using Domain.Aggregates.LaboratoryOrder;

namespace Application.Features.LaboratoryRequests.Commands.CreateLaboratoryRequestForPatient
{
    public class CreateLaboratoryRequestForPatientCommandHandler
        : IRequestHandler<CreateLaboratoryRequestForPatientCommand, ResultT<Guid>>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;
        private readonly IPatientProfileRepository _patientProfileRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLaboratoryRequestForPatientCommandHandler(
            ILaboratoryRequestRepository laboratoryRequestRepository,
            IPatientProfileRepository patientProfileRepository,
            ITestCategoryRepository testCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
            _patientProfileRepository = patientProfileRepository;
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultT<Guid>> Handle(CreateLaboratoryRequestForPatientCommand request, CancellationToken cancellationToken)
        {
            var patientProfile = await _patientProfileRepository.GetByUserIdAsync(request.patientId, cancellationToken);
            if (patientProfile is null)
                return PatientProfileErrors.NotFound(request.patientId);

            var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId, cancellationToken);
            if (category is null)
                return TestCategoryErrors.NotFound(request.testCategoryId);

            var requestResult = LaboratoryRequest.CreateForPatient(
                request.patientId, request.testCategoryId, request.clinicalDetails, request.appointmentId);

            if (requestResult.IsFailure)
                return requestResult.Error;

            await _laboratoryRequestRepository.AddAsync(requestResult.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return requestResult.value.Id;
        }
    }
}