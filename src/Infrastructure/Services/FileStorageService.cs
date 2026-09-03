using Application.Abstractions.FileStorage;
using Domain.Aggregates.Laboratory.LaboratoryRequest;
using Microsoft.Extensions.Configuration;
using SharedKernel.Shared;

namespace Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _rootPath;
        private static readonly string[] allowedFileExtensions = new[] {"pdf" };

        public FileStorageService(IConfiguration configuration)
        {
            _rootPath = configuration["FileStorage:RootPath"]!;

            Directory.CreateDirectory(_rootPath);
        }

        public async Task<ResultT<string>> StoreFileAsync(string fileName, Stream fileStream, string subFolder, CancellationToken cancellationToken = default)
        {
            var extension = Path.GetExtension(fileName)?.TrimStart('.').ToLowerInvariant();

            if(string.IsNullOrEmpty(extension) || !allowedFileExtensions.Contains(extension))
                return LaboratoryRequestErrors.LaboratoryResult.InvalidFileType;

            var folder = Path.Combine(_rootPath, subFolder);

            Directory.CreateDirectory(folder);

            var storedFileName = $"{Guid.NewGuid()}{extension}";

            var fullPath = Path.Combine(folder, storedFileName);

            await using (var destination = File.Create(fullPath))
            {
                await fileStream.CopyToAsync(destination, cancellationToken);
            }

            // Relative path is what gets persisted on the aggregate (PdfPath).
            return Path.Combine(subFolder, storedFileName).Replace('\\', '/');
        }

        public Task<ResultT<Stream>> GetFileAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteFileAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string relativePath)
        {
            throw new NotImplementedException();
        }
    }
}


