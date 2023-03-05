using Common.Tools;
using FileService.Domain.Entities;

namespace FileService.Domain;

public class FileDomainService
{
    private readonly IFileRepository repository;
    private readonly IStorageClient backupStorage;
    private readonly IStorageClient remoteStorage;
    public FileDomainService(IFileRepository repository,IEnumerable<IStorageClient> storageClients)
    {
        this.repository = repository;
        this.backupStorage = storageClients.First(c => c.StorageType == StorageType.Backup);
        this.remoteStorage = storageClients.First(c => c.StorageType == StorageType.Public);
    }

    public async Task<FileItem> UpLoadAsync(string fileName,
        Stream stream, CancellationToken cancellationToken)
    {
        string hash = HashHelper.ComputeSha256Hash(stream);
        long fileSize = stream.Length;
        string key = $"{hash}/{fileName}";
        var oldUploadItem = await repository.FindFileAsync(fileName,hash);
        if (oldUploadItem != null)
        {
            return oldUploadItem;
        }
        stream.Position = 0;
        Uri backupUrl = await backupStorage.SaveAsync(key, stream, cancellationToken);
        stream.Position = 0;
        Uri remoteUrl = await remoteStorage.SaveAsync(key, stream, cancellationToken);
        return FileItem.Create(fileName, fileSize, hash, backupUrl, remoteUrl);
    }
}
