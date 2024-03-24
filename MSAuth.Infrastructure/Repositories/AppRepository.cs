using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Infrastructure.Data;

namespace MSAuth.Infrastructure.Repositories
{
    public class AppRepository : BaseRepository<App>, IAppRepository
    {
        private readonly ApplicationDbContext _context;

        public AppRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<App?> GetByAppKeyAsync(string appKey)
        {
            return await _context.Apps.FirstOrDefaultAsync(a => a.AppKey == appKey);
        }
    }
}
