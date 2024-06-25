using MSAuth.Application.Interfaces.Infrastructure;

namespace MSAuth.Infrastructure.Services
{
    public class MockedEmailService : IEmailService
    {
        public async Task<bool> Send(string userEmail, string userConfirmationToken)
        {
            await Task.Delay(40000);
            Console.WriteLine("Email was sent! " + userEmail + " token: " + userConfirmationToken);
            return true;
        }
    }
}
