using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;
using MSAuth.Domain.IRepositories;
using MSAuth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Infrastructure.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly ApplicationDbContext _context;

        public AppRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<App> AddAsync(App app)
        {
            _context.Apps.Add(app);
            await _context.SaveChangesAsync();
            return app;
        }

        public async Task<App?> GetByAppKeyAsync(string appKey)
        {
            return await _context.Apps.FirstAsync(a => a.AppKey == appKey);
        }
    }
}
