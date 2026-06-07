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
    public class StaffAttendanceRepository : IStaffAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffAttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StaffAttendance attendance)
        {
            await _context.StaffAttendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StaffAttendance>> GetAllAsync()
        {
            return await _context.StaffAttendances.ToListAsync();
        }
    }
}
