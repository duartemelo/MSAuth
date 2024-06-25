using MSAuth.Domain.DTOs;

namespace MSAuth.Application.Interfaces
{
    public interface IUserConfirmationAppService
    {
        Task<string?> Create(UserConfirmationCreateDTO confirmationCreate, string appKey);
        Task<bool> Confirm(UserConfirmationValidateDTO validation, string appKey);
    }
}