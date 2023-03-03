namespace ProjectService.Domain.Entities;
public class Project
{
    public Guid Id { get;private set; }
    public string UserName { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public List<string>? Tags { get; private set; }
    public List<Guid>? ReadmeFiles { get; private set; }
    public IEnumerable<ProjectVersion>? ProjectVersions { get; private set; }
    public IEnumerable<ModelVersion>? ModelVersions { get; private set; }
    public IEnumerable<FirmwareVersion>? FirmwareVerisions { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime UpdateTime { get; private set; }
    private Project() { }

    public static Project Create(string userName, string name, string? description, List<string>? tags, List<Guid> readmeFiles)
    {
        Project project = new()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Name = name,
            Description = description,
            Tags = tags,
            ReadmeFiles = readmeFiles,
            CreationTime = DateTime.UtcNow,
            UpdateTime = DateTime.UtcNow
        };
        return project;
    }

    public Project ChangeTags(List<string>? tags) 
    { 
        this.Tags = tags;
        return this;
    }

    public Project ChangeDescription(string? description)
    {
        this.Description = description;
        return this;
    }

    public Project ChangeReadmeFiles(List<Guid>? readmeFiles)
    {
        this.ReadmeFiles = readmeFiles;
        return this;
    }

    public Project UpdateUpdateTime()
    {
        this.UpdateTime = DateTime.UtcNow;
        return this;
    }
}