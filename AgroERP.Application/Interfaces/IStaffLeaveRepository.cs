using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IStaffLeaveRepository
    {
        Task AddAsync(StaffLeave leave);
        Task<List<StaffLeave>> GetAllAsync();
        Task UpdateStatusAsync(int leaveId,string status);
    }
}
