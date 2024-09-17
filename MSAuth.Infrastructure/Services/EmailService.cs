using FluentEmail.Core;
using MSAuth.Application.Interfaces.Infrastructure;

namespace MSAuth.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail) 
        {
            _fluentEmail = fluentEmail;
        }

        public async Task Send(string userEmail, string userConfirmationToken)
        {
            await _fluentEmail
                .To(userEmail)
                .Subject("Email verification for MSAuth API")
                .Body($"To verify your address click here: {userConfirmationToken}")
                .SendAsync();
        }
    }
}
