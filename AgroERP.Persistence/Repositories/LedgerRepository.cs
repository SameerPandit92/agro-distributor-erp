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
    public class LedgerRepository : ILedgerRepository
    {
        private readonly ApplicationDbContext _context;

        public LedgerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetSalesAsync(int retailerId)
        {
            return await _context.Sales.Where(x => x.RetailerId == retailerId).ToListAsync();
        }

        public async Task<List<PaymentCollection>> GetPaymentsAsync(int retailerId)
        {
            return await _context.PaymentCollections.Where(x => x.RetailerId == retailerId).ToListAsync();
        }
    }
}
