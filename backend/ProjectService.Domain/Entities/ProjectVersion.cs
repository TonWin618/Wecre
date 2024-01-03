using System.Text.Json.Serialization;

namespace ProjectService.Domain.Entities;

public class ProjectVersion
{
    public Guid Id { get; private set; }
    [JsonIgnore]//Preventing circular References
    public Project Project { get; private set; }
    public string Name { get; private set; }
    public DateTime CreationTime { get; private set; }
    public string? Description { get; private set; }
    public long TotalDownloads { get; private set; }
    public FirmwareVersion? FirmwareVersion { get; private set; }
    public ModelVersion? ModelVersion { get; private set; }
    //Used when EFCore initializes the database
    private ProjectVersion() { }
    public ProjectVersion(Project project , string name, string description,
        FirmwareVersion firmwareVersion, ModelVersion modelVersion)
    {
        Id = Guid.NewGuid();
        Project= project;
        Name = name;
        CreationTime= DateTime.UtcNow;
        Description = description;
        TotalDownloads= 0;
        FirmwareVersion = firmwareVersion;
        ModelVersion = modelVersion;
    }

    public ProjectVersion ChangeDescription(string description)
    {
        this.Description = description;
        return this;
    }

    public ProjectVersion ChangeFirmwareVersion(FirmwareVersion firmwareVersion)
    {
        this.FirmwareVersion = firmwareVersion;
        return this;
    }

    public ProjectVersion ChangeModelVersion(ModelVersion modelVersion)
    {
        this.ModelVersion = modelVersion;
        return this;
    }
}
