namespace ProjectService.Domain.Entities;

public class ModelVersion
{
    public Guid Id { get; private set; }
    public Project Project { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public List<ProjectFile> Files { get; private set; }
    public long Downloads { get; private set; }
    private ModelVersion() { }
    public static ModelVersion Create(string versionName, Project project, List<ProjectFile> files)
    {
        ModelVersion modelVersion = new();
        modelVersion.Id = Guid.NewGuid();
        modelVersion.Project = project;
        modelVersion.Name = versionName;
        modelVersion.Files = files;
        return modelVersion;
    }

    public ModelVersion ChangeDonwloads()
    {
        this.Downloads++;
        return this;
    }

    public ModelVersion ChangeDescription(string? description)
    {
        this.Description = description;
        return this;
    }
}
