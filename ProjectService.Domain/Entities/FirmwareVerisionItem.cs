namespace ProjectService.Domain.Entities;

public class FirmwareVerisionItem
{
    public Guid Id { get; private set; }
    public string VersionName { get; private set; }
    public Guid[] FileUrls { get; private set; }
    public long Downloads { get; private set; }
    private FirmwareVerisionItem() { }
    public static FirmwareVerisionItem Create(string versionName, Guid[]? fileUrls)
    {
        FirmwareVerisionItem firmwareVerisionItem = new();
        firmwareVerisionItem.Id = Guid.NewGuid();
        firmwareVerisionItem.VersionName = versionName;
        firmwareVerisionItem.FileUrls = fileUrls;
        return firmwareVerisionItem;
    }
}
