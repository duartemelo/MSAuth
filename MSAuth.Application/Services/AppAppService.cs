using AutoMapper;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Services;

namespace MSAuth.Application.Services
{
    public class AppAppService
    {
        private readonly IAppService _appService;
        private readonly IMapper _mapper;

        public AppAppService(IAppService appService, IMapper mapper)
        {
            _appService = appService;
            _mapper = mapper;
        }

        public async Task<AppCreateDTO> CreateAppAsync()
        {
            var app = await _appService.CreateAppSync();
            return _mapper.Map<AppCreateDTO>(app);
        }
    }
}
