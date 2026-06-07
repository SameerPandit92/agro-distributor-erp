using AgroERP.Application.DTOs.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IStaffAttendanceService
    {
        Task AddAsync(CreateAttendanceDto dto);
    }
}
