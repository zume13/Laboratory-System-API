using SharedKernel.Shared;

namespace Application.Abstractions.FileStorage
{
    public interface IFileStorageService
    {
        Task<ResultT<string>> StoreFileAsync(string fileName, Stream fileStream, string subFolder, CancellationToken cancellationToken = default);
        ResultT<Stream> GetFile(string relativePath);
        ResultT<bool> DeleteFile(string relativePath);
        bool Exists(string relativePath);
    }
}
