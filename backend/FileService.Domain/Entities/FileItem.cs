namespace FileService.Domain.Entities;
public class FileItem
{
    public Guid Id { get; private set; }
    public long FileSizeInBytes { get; private set; }
    public string RelativePath { get; private set; }
    public Uri BackupUrl { get; private set; }
    public Uri RemoteUrl { get; private set; }
    private FileItem() { }
    public FileItem(string relativePath, Uri backupUrl, Uri remoteUrl, long fileSizeInBytes)
    {
        Id = Guid.NewGuid();
        RelativePath = relativePath;
        BackupUrl = backupUrl;
        RemoteUrl = remoteUrl;
        FileSizeInBytes = fileSizeInBytes;
    }
}
