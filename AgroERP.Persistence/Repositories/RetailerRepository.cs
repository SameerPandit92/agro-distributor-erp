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
    public class RetailerRepository : IRetailerRepository
    {
        private readonly ApplicationDbContext _context;

        public RetailerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Retailer>> GetAllAsync()
        {
            return await _context.Retailers.ToListAsync();
        }

        public async Task<Retailer?> GetByIdAsync(int id)
        {
            return await _context.Retailers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Retailer retailer)
        {
            await _context.Retailers.AddAsync(retailer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Retailer retailer)
        {
            _context.Retailers.Update(retailer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var retailer = await _context.Retailers.FindAsync(id);
            if (retailer is not null)
            {
                _context.Retailers.Remove(retailer);
                await _context.SaveChangesAsync();
            }
        }
    }
}