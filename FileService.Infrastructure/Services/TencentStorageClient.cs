using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using FileService.Domain;
using Microsoft.Extensions.Options;

namespace FileService.Infrastructure.Services;

public class TencentStorageClient:IStorageClient
{
    private IOptionsSnapshot<TencentStorageOptions> options;
    private CosXml cosXml;
    private QCloudCredentialProvider provider;
    public TencentStorageClient(IOptionsSnapshot<TencentStorageOptions> options)
    {
        this.options = options;
        CosXmlConfig config = new CosXmlConfig.Builder()
            .SetRegion(options.Value.Region).Build();
        this.provider = new DefaultQCloudCredentialProvider(options.Value.SecretId, options.Value.SecretKey, 600);
        this.cosXml = new CosXmlServer(config, provider);
    }

    public StorageType StorageType => StorageType.Public;

    public async Task<bool> RemoveAsync(string fullPath)
    {
        DeleteObjectRequest req = new(options.Value.BucketName,fullPath);
        cosXml.DeleteObject(req);
        return true;
    }

    public async Task<Uri?> SaveAsync(string relativePath, Stream content, CancellationToken cancellationToken = default)
    {
        try
        {
            string bucket = options.Value.BucketName;
            PutObjectRequest request = new PutObjectRequest(options.Value.BucketName, relativePath, content);
            PutObjectResult result = cosXml.PutObject(request);
            string url = options.Value.UrlRoot + relativePath;

            Console.WriteLine($"{result.eTag}");
            return new Uri(url);
        }catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
}
