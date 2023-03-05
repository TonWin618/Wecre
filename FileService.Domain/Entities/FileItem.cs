namespace FileService.Domain.Entities;
public class FileItem
{
    public Guid Id { get; private set; }
    public string FileName { get; private set; }
    public DateTime CreationTime { get;private set; }
    public long FileSizeInBytes { get;private set; }
    public string FileSHA256Hash { get; private set; }
    public Uri BackupUrl { get; private set; }
    public Uri RemoteUrl { get; private set; }
    public static FileItem Create(string fileName, long fileSizeInByte,  string fileSHA256Hash, 
        Uri BackupUrl, Uri remoteUrl)
    {
        FileItem item = new FileItem()
        {
            Id = Guid.NewGuid(),
            FileName = fileName,
            CreationTime = DateTime.UtcNow,
            FileSHA256Hash = fileSHA256Hash,
            FileSizeInBytes = fileSizeInByte,
            BackupUrl= BackupUrl,
            RemoteUrl= remoteUrl
        };
        return item;
    }
}