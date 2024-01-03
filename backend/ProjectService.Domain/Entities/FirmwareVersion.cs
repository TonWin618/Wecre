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

    //Used when EFCore initializes the database
    private FirmwareVersion() { }
    public FirmwareVersion (string versionName,Project project)
    {
        Id = Guid.NewGuid();
        Project= project;
        Name = versionName;
        Files = new List<ProjectFile>();
    }
    public FirmwareVersion AddFiles(List<ProjectFile> files)
    {
        Files = files;
        return this;
    }

    public FirmwareVersion ChangeDonwloads()
    {
        Downloads++;
        return this;
    }

    public FirmwareVersion ChangeDescription(string? description)
    {
        Description = description;
        return this;
    }
}
