using AutoMapper;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;

namespace MSAuth.Application.Services
{
    public class AppAppService
    {
        private readonly IAppService _appService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AppAppService(IAppService appService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _appService = appService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppCreateDTO> CreateAppAsync()
        {
            var app = _appService.CreateApp();
            if (!await _unitOfWork.CommitAsync()) {
                // TODO: add notification post failed
            }
            return _mapper.Map<AppCreateDTO>(app);
        }
    }
}
