namespace ProjectService.Domain.Entities;

public class ProjectVersion
{
    public Guid Id { get; private set; }
    public Project Project { get; private set; }
    public string Name { get; private set; }
    public DateTime CreationTime { get; private set; }
    public string? Description { get; private set; }
    public long TotalDownloads { get; private set; }
    public FirmwareVersion? FirmwareVersion { get; private set; }
    public ModelVersion? ModelVersion { get; private set; }
    private ProjectVersion() { }
    public static ProjectVersion Create(Project project , string name, string description,
        FirmwareVersion firmwareVersion, ModelVersion modelVersion)
    {
        ProjectVersion version = new()
        {
            Id = Guid.NewGuid(),
            Project= project,
            Name = name,
            CreationTime= DateTime.UtcNow,
            Description = description,
            TotalDownloads= 0,
            FirmwareVersion = firmwareVersion,
            ModelVersion = modelVersion
        };
        return version;
    }

    public ProjectVersion ChangeDescription(string description)
    {
        this.Description = description;
        return this;
    }

}
