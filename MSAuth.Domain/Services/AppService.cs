using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;

namespace MSAuth.Domain.Services
{
    public class AppService : IAppService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<App> CreateApp()
        {
            var app = new App();
            return await _unitOfWork.AppRepository.AddAsync(app);
        }
    }
}
