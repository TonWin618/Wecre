namespace FileService.Domain.Entities;
public class FileItem
{
    public Guid Id { get; private set; }
    //public string UserName { get; private set; }
    //public string ProjectName { get; private set; }
    //public string VersionName { get; private set; }
    //public string FileName { get; private set; }
    public FileIdentifier FileIdentifier { get; private set; }
    public DateTime CreationTime { get; set; }
    public long FileSizeInBytes { get; set; }
    public string FileSHA256Hash { get; private set; }
    public Uri BackupUrl { get; private set; }
    public Uri RemoteUrl { get; private set; }
    public static FileItem Create(FileIdentifier fileIdentifier, long fileSizeInByte,  string fileSHA256Hash, 
        Uri BackupUrl, Uri remoteUrl)
    {
        FileItem item = new FileItem()
        {
            Id = Guid.NewGuid(),
            FileIdentifier = fileIdentifier,
            CreationTime = DateTime.UtcNow,
            FileSHA256Hash = fileSHA256Hash,
            FileSizeInBytes = fileSizeInByte,
            BackupUrl= BackupUrl,
            RemoteUrl= remoteUrl
        };
        return item;
    }
}