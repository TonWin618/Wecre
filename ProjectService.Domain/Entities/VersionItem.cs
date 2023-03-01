namespace ProjectService.Domain.Entities;

public class VersionItem
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public string ProjectName { get; private set; }
    public string VersionName { get; private set; }
    public DateTime CreationTime { get; private set; }
    public string? Description { get; private set; }
    public long TotalDownloads { get; private set; }
    public IEnumerable<Guid>? FirmwareVersions { get; private set; }
    public IEnumerable<Guid>? ModelVersions { get; private set; }
    public static VersionItem Create(string userName,string projectName, string versionName, string description, 
        IEnumerable<Guid> firmwares, IEnumerable<Guid> models)
    {
        VersionItem versionItem = new()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            ProjectName = projectName,
            VersionName = versionName,
            CreationTime= DateTime.UtcNow,
            Description = description,
            TotalDownloads= 0,
            FirmwareVersions = firmwares,
            ModelVersions = models
        };
        return versionItem;
    }

    public VersionItem ChangeDescription(string description)
    {
        this.Description = description;
        return this;
    }
    public VersionItem ChangeFirmwares(IEnumerable<Guid> firmwares)
    {
        this.FirmwareVersions = firmwares.ToList();
        return this;
    }
    public VersionItem ChangeModels(IEnumerable<Guid> models)
    {
        this.ModelVersions = models.ToList();
        return this;
    }

}
