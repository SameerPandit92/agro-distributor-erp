using AgroERP.Application.DTOs.Staff;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IStaffService
    {
        Task AddAsync(CreateStaffDto dto);
        Task<List<Staff>> GetAllAsync();
    }
}
