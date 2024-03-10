using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.UnitOfWork;

namespace MSAuth.Application.Services
{
    public class EmailAppService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendUserConfirmationJob(int userId, string appKey)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, appKey);

            if (user != null && user.Email != null)
            {
                var emailResult = await SendEmailMock(user.Email);
                if (emailResult == true)
                {
                    var userConfirmationToCreate = new UserConfirmation(user);
                    _unitOfWork.UserConfirmationRepository.Add(userConfirmationToCreate);  
                }
            }

            if (!await _unitOfWork.CommitAsync())
            {
                // TODO: add notification post failed
            }
        }

        private static async Task<bool> SendEmailMock(string userEmail)
        {
            await Task.Delay(40000);
            Console.WriteLine("Email was sent! " + userEmail);
            return true;
        }
    }
}
