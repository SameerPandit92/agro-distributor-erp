using AgroERP.Application.DTOs.Leave;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class StaffLeaveService : IStaffLeaveService
    {
        private readonly IStaffLeaveRepository _repository;

        public StaffLeaveService(IStaffLeaveRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(CreateStaffLeaveDto dto)
        {
            var leave = new StaffLeave
                       {
                          StaffId =dto.StaffId,
                          LeaveDate = dto.LeaveDate,
                          Reason =dto.Reason
                       };
            await _repository.AddAsync(leave);
        }

        public async Task<List<StaffLeave>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateStatusAsync(int leaveId, string status)
        {
            await _repository.UpdateStatusAsync(leaveId,status);
        }
    }
}
