using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Infrastructure.Data;

namespace MSAuth.Infrastructure.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly ApplicationDbContext _context;

        public AppRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public App Add(App app)
        {
            _context.Apps.Add(app);
            return app;
        }

        public async Task<App?> GetByAppKeyAsync(string appKey)
        {
            return await _context.Apps.FirstOrDefaultAsync(a => a.AppKey == appKey);
        }
    }
}
