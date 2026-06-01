using AgroERP.Application.DTOs.Retailer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IRetailerService
    {
        Task<List<RetailerDto>>GetAllAsync();
        Task<RetailerDto?>GetByIdAsync(int id);
        Task AddAsync(CreateRetailerDto dto);
        Task UpdateAsync(UpdateRetailerDto dto);
        Task DeleteAsync(int id);
    }
}
