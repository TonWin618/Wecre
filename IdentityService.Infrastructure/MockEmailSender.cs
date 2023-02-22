using IdentityService.Domain;
using Microsoft.Extensions.Logging;

namespace IdentityService.Infrastructure
{
    internal class MockEmailSender : IEmailSender
    {
        private readonly ILogger<MockEmailSender> logger;
        public MockEmailSender(ILogger<MockEmailSender> logger)
        {
            this.logger = logger;
        }
        public Task SendChangeTokenAsync(string toEmail, string AuthToken, string changeToken)
        {
            return Task.CompletedTask;
        }
    }
}
