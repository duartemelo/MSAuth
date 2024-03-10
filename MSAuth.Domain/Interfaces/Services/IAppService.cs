using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.Interfaces.Services
{
    public interface IAppService
    {
        Task<App> CreateAppSync();
    }
}
