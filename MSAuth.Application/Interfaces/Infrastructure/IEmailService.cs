namespace MSAuth.Application.Interfaces.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> Send(string userEmail);
    }
}
