using IdentityService.Domain;
using Microsoft.Extensions.Logging;

namespace IdentityService.Infrastructure.Services;

public class MockEmailSender : IEmailSender
{
    private readonly ILogger<MockEmailSender> logger;
    public MockEmailSender(ILogger<MockEmailSender> logger)
    {
        this.logger = logger;
    }
    public async Task<bool> SendTokenAsync(string toEmail, string changeToken)
    {
        logger.LogInformation($"toEmail:{toEmail},ChangeToken:{changeToken}");
        return true;
    }
}
