using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using AgroERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Persistence.Repositories
{
    public class StaffLeaveRepository : IStaffLeaveRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffLeaveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StaffLeave leave)
        {
            await _context.StaffLeaves.AddAsync(leave);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StaffLeave>> GetAllAsync()
        {
            return await _context.StaffLeaves.ToListAsync();
        }
        public async Task UpdateStatusAsync(int leaveId,string status)
        {
            var leave = await _context.StaffLeaves.FindAsync(leaveId);
            if (leave == null) throw new Exception("Leave not found");
            leave.Status =status;
            await _context.SaveChangesAsync();
        }
    }
}
