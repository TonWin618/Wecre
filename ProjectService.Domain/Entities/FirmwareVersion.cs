using System.Text.Json.Serialization;

namespace ProjectService.Domain.Entities;

public class FirmwareVersion
{
    public Guid Id { get; private set; }
    [JsonIgnore]//Preventing circular References
    public Project Project { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public List<ProjectFile> Files { get; private set; }
    public long Downloads { get; private set; }
    private FirmwareVersion() { }
    public static FirmwareVersion Create(string versionName,Project project, List<ProjectFile> files)
    {
        FirmwareVersion firmwareVerision = new();
        firmwareVerision.Id = Guid.NewGuid();
        firmwareVerision.Project= project;
        firmwareVerision.Name = versionName;
        firmwareVerision.Files = files;
        return firmwareVerision;
    }
    public FirmwareVersion ChangeFiles(List<ProjectFile> files)
    {
        this.Files = files;
        return this;
    }

    public FirmwareVersion ChangeDonwloads()
    {
        this.Downloads++;
        return this;
    }

    public FirmwareVersion ChangeDescription(string? description)
    {
        this.Description = description;
        return this;
    }
}
