using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryRequest;
using Domain.Aggregates.Laboratory.TestCategory;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Commands.CreateLaboratoryRequestForWalkIn
{
    public class CreateLaboratoryRequestForWalkInCommandHandler
        : IRequestHandler<CreateLaboratoryRequestForWalkInCommand, ResultT<Guid>>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLaboratoryRequestForWalkInCommandHandler(
            ILaboratoryRequestRepository laboratoryRequestRepository,
            ITestCategoryRepository testCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultT<Guid>> Handle(CreateLaboratoryRequestForWalkInCommand request, CancellationToken cancellationToken)
        {
            var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId, cancellationToken);
            if (category is null)
                return TestCategoryErrors.NotFound(request.testCategoryId);

            var requestResult = LaboratoryRequest.CreateForWalkIn(
                request.physicalPatientId, request.testCategoryId, request.clinicalDetails);

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