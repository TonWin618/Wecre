namespace IdentityService.Domain;

public interface IEmailSender
{
    public Task SendChangeTokenAsync(string toEmail, string changeToken);
}
