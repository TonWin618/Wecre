using FileService.Domain;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace FileService.WebAPI.Controllers;

public class SMBStorageClient:IStorageClient
{
    private IOptionsSnapshot<SMBStorageOptions> options;
    public SMBStorageClient(IOptionsSnapshot<SMBStorageOptions> options)
    {
        this.options = options;
    }

    public StorageType StorageType => StorageType.Backup;

    public async Task<bool> RemoveAsync(string fullPath)
    {
    //D:\Temp\upload\string\test\2(1).txt
        if (!File.Exists(fullPath))
        {
            return false;
        }
        try
        {
            File.Delete(fullPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return true;
    }

    public async Task<Uri> SaveAsync(string relativePath, Stream content, CancellationToken cancellationToken = default)
    {
        string workingDir = options.Value.WorkingDir;
        string fullPath = Path.Combine(workingDir, relativePath);
        string? fullDir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(fullDir))
        {
            Directory.CreateDirectory(fullDir);
        }
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        using Stream outStream = File.OpenWrite(fullPath);
        await content.CopyToAsync(outStream,cancellationToken);
        return new Uri(fullPath);
    }
}
