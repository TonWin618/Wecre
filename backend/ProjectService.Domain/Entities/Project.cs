namespace ProjectService.Domain.Entities;
public class Project
{
    public Guid Id { get;private set; }
    public string UserName { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public List<string>? Tags { get; private set; }
    public List<ProjectFile>? ReadmeFiles { get; private set; }
    public List<ProjectVersion>? ProjectVersions { get; private set; }
    public List<ModelVersion>? ModelVersions { get; private set; }
    public List<FirmwareVersion>? FirmwareVerisions { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime UpdateTime { get; private set; }

    //Used when EFCore initializes the database
    private Project() { }
    public Project(string userName, string name, string? description, List<string>? tags)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Name = name;
        Description = description;
        Tags = tags;
        ReadmeFiles = null;
        CreationTime = DateTime.UtcNow;
        UpdateTime = DateTime.UtcNow;
    }

    public Project ChangeTags(List<string>? tags) 
    { 
        Tags = tags;
        return this;
    }

    public Project ChangeDescription(string? description)
    {
        Description = description;
        return this;
    }

    public Project ChangeReadmeFiles(List<ProjectFile>? readmeFiles)
    {
        ReadmeFiles = readmeFiles;
        return this;
    }

    public Project ChangeUpdateTime()
    {
        UpdateTime = DateTime.UtcNow;
        return this;
    }
}