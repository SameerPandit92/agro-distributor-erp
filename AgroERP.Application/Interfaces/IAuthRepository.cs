using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<AppUser?> GetByUsernameAsync( string username);
        Task AddUserAsync(AppUser user);
    }
}
