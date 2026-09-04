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

            return Path.Combine(subFolder, storedFileName).Replace('\\', '/');
        }

        public async Task<ResultT<Stream>> GetFileAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            var fullPath = ResolveSafePath(relativePath);   

            if(fullPath.IsFailure)
                return fullPath.Error;

            if (!File.Exists(fullPath.value))
                return LaboratoryRequestErrors.LaboratoryResult.FileNotFound(relativePath);

            Stream stream = File.OpenRead(fullPath.value);

            return ResultT<Stream>.Success(stream);
        }

        public async Task<ResultT<bool>> DeleteFileAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            var fullPath = ResolveSafePath(relativePath);

            if (fullPath.IsFailure)
                return fullPath.Error;

            if (!File.Exists(fullPath.value))
                return LaboratoryRequestErrors.LaboratoryResult.FileNotFound(relativePath);

            File.Delete(fullPath.value);

            return ResultT<bool>.Success(true); 
        }

        public bool Exists(string relativePath)
        {
            var fullPath = ResolveSafePath(relativePath);

            if (fullPath.IsFailure)
                return false;

            return File.Exists(fullPath.value);
        }

        private ResultT<string> ResolveSafePath(string relativePath)
        {
            var fullPath = Path.GetFullPath(Path.Combine(_rootPath, relativePath));

            if (!fullPath.StartsWith(Path.GetFullPath(_rootPath), StringComparison.OrdinalIgnoreCase))
                return LaboratoryRequestErrors.LaboratoryResult.InvalidFilePath;

            return fullPath;
        }
    }
}


