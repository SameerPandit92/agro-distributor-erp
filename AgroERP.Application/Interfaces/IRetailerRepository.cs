using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IRetailerRepository
    {
        Task<List<Retailer>> GetAllAsync();

        Task<Retailer?> GetByIdAsync(int id);

        Task AddAsync(Retailer retailer);

        Task UpdateAsync(Retailer retailer);

        Task DeleteAsync(int id);
    }
}
