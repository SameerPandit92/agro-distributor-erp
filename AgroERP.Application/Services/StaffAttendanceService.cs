using AgroERP.Application.DTOs.Attendance;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class StaffAttendanceService : IStaffAttendanceService
    {
        private readonly IStaffAttendanceRepository _repository;

        public StaffAttendanceService(IStaffAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(CreateAttendanceDto dto)
        {
            var attendance = new StaffAttendance
            {
                StaffId = dto.StaffId,
                Status = dto.Status
            };
            await _repository.AddAsync(attendance);
        }
    }
}

