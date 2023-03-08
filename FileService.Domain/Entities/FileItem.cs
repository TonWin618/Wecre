namespace FileService.Domain.Entities;
public class FileItem
{
    public Guid Id { get; private set; }
    public long FileSizeInBytes { get; private set; }
    public string RelativePath { get; private set; }
    public Uri BackupUrl { get; private set; }
    public Uri RemoteUrl { get; private set; }
    private FileItem() { }
    public static FileItem Create(string relativePath, 
        Uri backupUrl, Uri remoteUrl, long fileSizeInBytes)
    {
        FileItem item = new FileItem()
        {
            Id = Guid.NewGuid(),
            RelativePath = relativePath,
            BackupUrl = backupUrl,
            RemoteUrl= remoteUrl,
            FileSizeInBytes= fileSizeInBytes
        };
        return item;
    }
}
