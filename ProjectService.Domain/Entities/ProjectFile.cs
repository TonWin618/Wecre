namespace ProjectService.Domain.Entities;

public class ProjectFile
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ItemId { get; private set; }
    public string? Description { get; private set; }
    public static ProjectFile Create(string name, Guid id, string? description)
    {
        ProjectFile file = new ProjectFile();
        file.Id = Guid.NewGuid();
        file.Name = name;
        file.Id = id;
        file.Description = description;
        return file;
    }
}
