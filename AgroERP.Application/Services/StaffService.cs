using AgroERP.Application.DTOs.Staff;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _repository;

        public StaffService(IStaffRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(CreateStaffDto dto)
        {
            var staff = new Staff
                {
                    FullName = dto.FullName,
                    MobileNumber = dto.MobileNumber,
                    Role = dto.Role,
                    Salary = dto.Salary
                };

            await _repository.AddAsync(staff);
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
