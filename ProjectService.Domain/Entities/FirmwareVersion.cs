namespace ProjectService.Domain.Entities;

public class FirmwareVersion
{
    public Guid Id { get; private set; }
    public Project Project { get; private set; }
    public string Name { get; private set; }
    public List<Guid> Files { get; private set; }
    public long Downloads { get; private set; }
    private FirmwareVersion() { }
    public static FirmwareVersion Create(string versionName,Project project, List<Guid> files)
    {
        FirmwareVersion firmwareVerision = new();
        firmwareVerision.Id = Guid.NewGuid();
        firmwareVerision.Project= project;
        firmwareVerision.Name = versionName;
        firmwareVerision.Files = files;
        return firmwareVerision;
    }
}
