using IdentityService.Domain;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Threading;

namespace IdentityService.Infrastructure.Services;

public class SMTPEmailSender : IEmailSender
{
    private readonly IOptionsSnapshot<SMTPEmailOptions> options;

    public SMTPEmailSender(IOptionsSnapshot<SMTPEmailOptions> options)
    {
        this.options = options;
    }

    public async Task<bool> SendTokenAsync(string toEmail, string changeToken)
    {
        MailMessage mail = new MailMessage(options.Value.Email, toEmail);
        mail.Subject = "AuthCode from wecre.net";
        mail.Body = changeToken;
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = options.Value.Host;
        smtpClient.Port = options.Value.Port;
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential(options.Value.Email, options.Value.Password);
        CancellationToken cancellationToken = new();
        try
        {
            await smtpClient.SendMailAsync(mail, cancellationToken);
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
}