namespace FileService.Domain;
public interface IStorageClient
{
    StorageType StorageType { get; }
    Task<Uri> SaveAsync(string key,Stream content, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(string key);
}
