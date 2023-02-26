namespace ProjectService.Domain.Entities;

public class VersionItem
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public string ProjectName { get; private set; }
    public string Version { get; private set; }
    public DateTime CreationTime { get; private set; }
    public string? Description { get; private set; }
    public long TotalDownloads { get; private set; }
    public IEnumerable<Guid>? Firmwares { get; private set; }
    public IEnumerable<Guid>? Models { get; private set; }
    public static VersionItem Create(string userName,string projectName, string version, string description, 
        IEnumerable<Guid> firmwares, IEnumerable<Guid> models)
    {
        VersionItem versionItem = new()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            ProjectName = projectName,
            Version = version,
            CreationTime= DateTime.UtcNow,
            Description = description,
            TotalDownloads= 0,
            Firmwares = firmwares,
            Models = models
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
        this.Firmwares = firmwares.ToList();
        return this;
    }
    public VersionItem ChangeModels(IEnumerable<Guid> models)
    {
        this.Models = models.ToList();
        return this;
    }

}
