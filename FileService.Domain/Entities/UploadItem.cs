namespace FileService.Domain.Entities;
public class UploadItem
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreationTime { get; set; }
    public long FileSizeInBytes { get; set; }
    public string FileName { get; private set; }
    public string FileSHA256Hash { get; private set; }
    public Uri BackupUrl { get; private set; }
    public Uri remoteUrl { get; private set; }
    public static UploadItem Create(Guid id,long fileSizeInByte, string fileName, string fileSHA256Hash, Uri BackupUrl, Uri remoteUrl)
    {
        UploadItem item = new UploadItem()
        {
            Id = id,
            CreationTime = DateTime.UtcNow,
            FileName = fileName,
            FileSHA256Hash = fileSHA256Hash,
            FileSizeInBytes = fileSizeInByte,
            BackupUrl= BackupUrl,
            remoteUrl= remoteUrl
        };
        return item;
    }
}