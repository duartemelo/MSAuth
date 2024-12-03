namespace MSAuth.Application.Interfaces.Infrastructure
{
    public interface IEmailService
    {
        Task Send(string userEmail, string userConfirmationToken);
    }
}
