namespace ProjectService.Domain.Entities;
public class Project
{
    public Guid Id { get;private set; }
    public string UserName { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Tags { get; private set; }
    public Guid ReadmeUrl { get; private set; }
    public IEnumerable<ProjectVersion>? ProjectVersions { get; private set; }
    public IEnumerable<ModelVersion>? ModelVersions { get; private set; }
    public IEnumerable<FirmwareVerision>? FirmwareVerisions { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime UpdateTime { get; private set; }

    public static Project Create(string userName, string Name, string? description, string? tags, Guid readmeUrl)
    {
        Project projectItem = new()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Name = Name,
            Description = description,
            Tags = tags,
            ReadmeUrl = readmeUrl,
            CreationTime = DateTime.UtcNow,
            UpdateTime = DateTime.UtcNow
        };
        return projectItem;
    }

    public Project ChangeTags(string? tags) 
    { 
        this.Tags = tags;
        return this;
    }

    public Project ChangeDescription(string? description)
    {
        this.Description = description;
        return this;
    }

    public Project UpdateUpdateTime()
    {
        this.UpdateTime = DateTime.UtcNow;
        return this;
    }
}