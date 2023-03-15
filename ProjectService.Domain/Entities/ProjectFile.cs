namespace ProjectService.Domain.Entities;

public class ProjectFile
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string RelativePath { get; private set; }
    public long SizeInBytes { get; private set; }
    public string? Description { get; private set; }
    public long Downloads { get; private set; }

    private ProjectFile() { }
    public static ProjectFile Create(string name, string relativePath, long sizeInBytes, string? description)
    {
        ProjectFile file = new ProjectFile();
        file.Id = Guid.NewGuid();
        file.Name = name;
        file.RelativePath = relativePath;
        file.SizeInBytes = sizeInBytes;
        file.Description = description;
        file.Downloads = 0;
        return file;
    }
}
