namespace ProjectService.Domain.Entities;

public class FirmwareVerision
{
    public Guid Id { get; private set; }
    public Project Project { get; private set; }
    public string Name { get; private set; }
    public List<Guid> Files { get; private set; }
    public long Downloads { get; private set; }
    private FirmwareVerision() { }
    public static FirmwareVerision Create(string versionName,Project project, List<Guid>? files)
    {
        FirmwareVerision firmwareVerisionItem = new();
        firmwareVerisionItem.Id = Guid.NewGuid();
        firmwareVerisionItem.Name = versionName;
        firmwareVerisionItem.Files = files;
        return firmwareVerisionItem;
    }
}
