using AgroERP.Application.DTOs.Leave;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IStaffLeaveService
    {
        Task AddAsync(CreateStaffLeaveDto  dto);
        Task<List<StaffLeave>> GetAllAsync();
        Task UpdateStatusAsync(int leaveId, string status);
    }
}
