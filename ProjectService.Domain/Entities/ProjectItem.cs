namespace ProjectService.Domain.Entities;
public class ProjectItem
{
    public Guid Id { get;private set; }
    public string UserName { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime UpdateTime { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Tags { get; private set; }
    public Uri? ReadmeUrl { get; private set; }
    public string? Versions { get; private set; }

    public static ProjectItem Create(string userName, string name, string? description, string? tags, Uri readmeUrl)
    {
        ProjectItem projectItem = new()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            CreationTime = DateTime.UtcNow,
            UpdateTime = DateTime.UtcNow,
            Name = name,
            Description = description,
            Tags = tags,
            ReadmeUrl = readmeUrl,
            Versions = ""
        };
        return projectItem;
    }

    public ProjectItem ChangeVersions(string? versions)
    {
        this.Versions = versions;
        return this;
    }

    public ProjectItem ChangeTags(string? tags) 
    { 
        this.Tags = tags;
        return this;
    }

    public ProjectItem ChangeDescription(string? description)
    {
        this.Description = description;
        return this;
    }

    public ProjectItem UpdateUpdateTime()
    {
        this.UpdateTime = DateTime.UtcNow;
        return this;
    }
}