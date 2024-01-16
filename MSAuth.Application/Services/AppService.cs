using MSAuth.Application.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Application.Services
{
    public class AppService
    {
        private readonly IAppRepository _appRepository;

        public AppService(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public async Task<AppCreateDTO> CreateAppAsync()
        {
            var app = new App();

            await _appRepository.AddAsync(app);

            return new AppCreateDTO
            {
                AppKey = app.AppKey
            };
        }
    }
}
