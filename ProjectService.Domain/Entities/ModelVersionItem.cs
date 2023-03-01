namespace ProjectService.Domain.Entities;

public class ModelVersionItem
{
    public Guid Id { get; private set; }
    public string VersionName { get; private set; }
    public Guid[] FileUrls { get; private set; }
    public long Downloads { get; private set; }
    private ModelVersionItem() { }
    public static ModelVersionItem Create(string versionName, Guid[]? fileUrls)
    {
        ModelVersionItem modelVersionItem = new();
        modelVersionItem.Id = Guid.NewGuid();
        modelVersionItem.VersionName = versionName;
        modelVersionItem.FileUrls = fileUrls;
        return modelVersionItem;
    }
}
