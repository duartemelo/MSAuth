using MSAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.Interfaces.Persistence.Repositories
{
    public interface IUserConfirmationRepository : IBaseRepository<UserConfirmation>
    {
        Task<UserConfirmation?> GetByTokenAsync(string token);
    }
}
