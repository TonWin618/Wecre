namespace IdentityService.Domain;

public interface IEmailSender
{
    public Task SendTokenAsync(string toEmail, string changeToken);
}
