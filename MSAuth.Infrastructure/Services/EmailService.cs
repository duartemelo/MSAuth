using MSAuth.Application.Interfaces.Infrastructure;

namespace MSAuth.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> Send(string userEmail)
        {
            await Task.Delay(40000);
            Console.WriteLine("Email was sent! " + userEmail);
            return true;
        }
    }
}
