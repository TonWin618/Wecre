namespace IdentityService.Domain;

public interface IEmailSender
{
    public Task<bool> SendTokenAsync(string toEmail, string changeToken);
}
