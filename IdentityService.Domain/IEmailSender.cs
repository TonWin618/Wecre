namespace IdentityService.Domain;

internal interface IEmailSender
{
    public Task SendChangeTokenAsync(string toEmail, string subject, string body);
}
