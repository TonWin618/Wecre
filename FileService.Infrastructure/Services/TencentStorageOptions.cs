namespace FileService.Infrastructure.Services;

public class TencentStorageOptions
{
    public string BucketName { get; set; }
    public string SecretId { get; set; }
    public string SecretKey { get; set; }
    public string UrlRoot { get; set; }
    public string region { get; set; }
}
