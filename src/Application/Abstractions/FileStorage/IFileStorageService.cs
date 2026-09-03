using SharedKernel.Shared;

namespace Application.Abstractions.FileStorage
{
    public interface IFileStorageService
    {
        Task<ResultT<string>> StoreFileAsync(string fileName, Stream fileStream, string subFolder, CancellationToken cancellationToken = default);
        Task<ResultT<Stream>> GetFileAsync(string relativePath, CancellationToken cancellationToken = default);
        Task<Result> DeleteFileAsync(string relativePath, CancellationToken cancellationToken = default);
        bool Exists(string relativePath);
    }
}
