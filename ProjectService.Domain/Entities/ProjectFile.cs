namespace ProjectService.Domain.Entities;

public class ProjectFile
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Url { get; private set; }
    public long SizeInBytes { get; private set; }
    public string? Description { get; private set; }
    public long Downloads { get; private set; }
    public static ProjectFile Create(string name, string url, long sizeInBytes, string? description)
    {
        ProjectFile file = new ProjectFile();
        file.Id = Guid.NewGuid();
        file.Name = name;
        file.Url = url;
        file.SizeInBytes = sizeInBytes;
        file.Description = description;
        file.Downloads = 0;
        return file;
    }
}
