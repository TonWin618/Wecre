namespace ProjectService.Domain.Entities;

public class ProjectVersion
{
    public Guid Id { get; private set; }
    public Project Project { get; private set; }
    public string Name { get; private set; }
    public DateTime CreationTime { get; private set; }
    public string? Description { get; private set; }
    public long TotalDownloads { get; private set; }
    public FirmwareVerision? FirmwareVersions { get; private set; }
    public ModelVersion? ModelVersions { get; private set; }
    public static ProjectVersion Create(Project project , string name, string description,
        FirmwareVerision firmwareVersion, ModelVersion modelVersion)
    {
        ProjectVersion versionItem = new()
        {
            Id = Guid.NewGuid(),
            Project= project,
            Name = name,
            CreationTime= DateTime.UtcNow,
            Description = description,
            TotalDownloads= 0,
            FirmwareVersions = firmwareVersion,
            ModelVersions = modelVersion
        };
        return versionItem;
    }

    public ProjectVersion ChangeDescription(string description)
    {
        this.Description = description;
        return this;
    }

}
