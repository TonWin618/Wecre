namespace ProjectService.Domain.Entities;

public class ModelVersion
{
    public Guid Id { get; private set; }
    public Project Project { get; private set; }
    public string Name { get; private set; }
    public List<Guid> Files { get; private set; }
    public long Downloads { get; private set; }
    private ModelVersion() { }
    public static ModelVersion Create(string versionName, Project project, List<Guid>? files)
    {
        ModelVersion modelVersionItem = new();
        modelVersionItem.Id = Guid.NewGuid();
        modelVersionItem.Project = project;
        modelVersionItem.Name = versionName;
        modelVersionItem.Files = files;
        return modelVersionItem;
    }
}
