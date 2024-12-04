using MSAuth.Domain.DTOs;

namespace MSAuth.Application.Interfaces
{
    public interface IUserConfirmationAppService
    {
        Task<string?> Create(UserConfirmationCreateDTO confirmationCreate);
        Task<bool> Confirm(UserConfirmationValidateDTO validation);
        Task SendUserConfirmationJob(string userEmail, string userConfirmationToken);
    }
}