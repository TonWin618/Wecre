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

    public async Task<FileItem> UpLoadAsync(FileIdentifier fileIdentifier,
        Stream stream, CancellationToken cancellationToken)
    {
        string hash = HashHelper.ComputeSha256Hash(stream);
        long fileSize = stream.Length;
        string key = $"{fileIdentifier.UserName}/{fileIdentifier.ProjectName}/{fileIdentifier.FileType.ToString()}/{fileIdentifier.VersionName}/{fileIdentifier.FileName}";
        var oldUploadItem = await repository.FindFileAsync(fileIdentifier);
        if (oldUploadItem != null)
        {
            return oldUploadItem;
        }
        stream.Position = 0;
        Uri backupUrl = await backupStorage.SaveAsync(key, stream, cancellationToken);
        stream.Position = 0;
        Uri remoteUrl = await remoteStorage.SaveAsync(key, stream, cancellationToken);
        return FileItem.Create(fileIdentifier, fileSize, hash, backupUrl, remoteUrl);
    }
}
