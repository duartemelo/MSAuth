﻿using MSAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IAppRepository
    {
        App Add(App app);
        Task<App?> GetByAppKeyAsync(string appKey);
    }
}