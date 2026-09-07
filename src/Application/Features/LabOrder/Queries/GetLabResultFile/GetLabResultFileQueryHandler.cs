using Application.Abstractions.FileStorage;
using MediatR;
using SharedKernel.Constants;
using SharedKernel.Shared;
using System.IO.Enumeration;

namespace Application.Features.LabOrder.Queries.GetLabResultFile
{
    internal class GetLabResultFileQueryHandler : IRequestHandler<GetLabResultFileQuery, ResultT<GetLabResultFileDto>>
    {
        private readonly IFileStorageService _fileStorageService;
        public GetLabResultFileQueryHandler(IFileStorageService service)
        {
            _fileStorageService = service;
        }
        public async Task<ResultT<GetLabResultFileDto>> Handle(GetLabResultFileQuery request, CancellationToken cancellationToken)
        {
            var file = _fileStorageService.GetFile(request.relativePath);

            if (file.IsFailure)
                return file.Error;

            var fileName = Path.GetFileName(request.relativePath);

            return ResultT<Stream>.Success(new GetLabResultFileDto(file.value, SystemConstants.SupportedFileExts.pdf, fileName));
        }
    }
}
