using AutoMapper;
using MSAuth.Application.Interfaces;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class AppAppService : IAppAppService
    {
        private readonly IAppService _appService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificationContext;

        public AppAppService(IAppService appService, IMapper mapper, IUnitOfWork unitOfWork, NotificationContext notificationContext)
        {
            _appService = appService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }

        public async Task<AppCreateDTO> CreateAppAsync()
        {
            var app = await _appService.CreateApp();
            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
            }
            return _mapper.Map<AppCreateDTO>(app);
        }
    }
}
