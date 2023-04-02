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

    /// <summary>
    /// upload a file, make sure the target file the filepath refer to does not exists before calling the method.
    /// </summary>
    /// <param name="relativePath"></param>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<FileItem> UpLoadAsync(string relativePath,
        Stream stream, CancellationToken cancellationToken)
    {
        long fileSize = stream.Length;
        var oldUploadItem = await repository.FindFileAsync(relativePath);
        if (oldUploadItem != null)
        {
            return oldUploadItem;
        }

        stream.Position = 0;
        Uri backupUrl = await backupStorage.SaveAsync(relativePath, stream, cancellationToken);
        stream.Position = 0;
        Uri remoteUrl = await remoteStorage.SaveAsync(relativePath, stream, cancellationToken);

        return new FileItem(relativePath, backupUrl, remoteUrl,fileSize);
    }

    /// <summary>
    /// delete a file
    /// </summary>
    /// <param name="relativePath"></param>
    /// <returns></returns>
    public async Task<bool> DeleteFileAsync(string relativePath)
    {
        FileItem? fileItem = await repository.FindFileAsync(relativePath);
        if(fileItem == null)
        {
            return false;
        }
        //Do not change the order of deletion
        if (false == await backupStorage.RemoveAsync(fileItem.BackupUrl.LocalPath.ToString()))
        {
            return false;
        }
        if(false == await remoteStorage.RemoveAsync(fileItem.RemoteUrl.LocalPath.ToString()))
        {
            return false;
        }
        await repository.RemoveFileAsync(fileItem);
        return true;
    }
}
