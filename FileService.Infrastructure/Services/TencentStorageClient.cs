using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Transfer;
using FileService.Domain;
using Microsoft.Extensions.Options;

namespace FileService.Infrastructure.Services;

public class TencentStorageClient:IStorageClient
{
    private readonly IOptionsSnapshot<TencentStorageOptions> options;
    private CosXml cosXml;
    public TencentStorageClient(IOptionsSnapshot<TencentStorageOptions> options, CosXml cosXml)
    {
        this.options = options;
        this.cosXml = cosXml;
    }

    public StorageType StorageType => StorageType.Public;

    public async Task<Uri> SaveAsync(string key, Stream content, CancellationToken cancellationToken = default)
    {
        PutObjectRequest request = new PutObjectRequest(options.Value.BucketName, key, content);
        PutObjectResult result = cosXml.PutObject(request);
        string url = options.Value.UrlRoot + key;
        return new Uri(url);
    }
}
