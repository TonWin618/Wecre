namespace ProjectService.Domain.Entities;

public class ProjectFile
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string RelativePath { get; private set; }
    public long SizeInBytes { get; private set; }
    public string? Description { get; private set; }
    public long Downloads { get; private set; }

    //Used when EFCore initializes the database
    private ProjectFile() { }
    public ProjectFile(string name, string relativePath, long sizeInBytes, string? description)
    {
        Id = Guid.NewGuid();
        Name = name;
        RelativePath = relativePath;
        SizeInBytes = sizeInBytes;
        Description = description;
        Downloads = 0;
    }
}
