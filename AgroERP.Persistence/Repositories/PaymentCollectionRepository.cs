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
    public class PaymentCollectionRepository : IPaymentCollectionRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentCollectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PaymentCollection paymentCollection)
        {
            var retailer = await _context.Retailers.FirstOrDefaultAsync(x => x.Id == paymentCollection.RetailerId);

            if (retailer == null)
            {
                throw new Exception("Retailer not found");
            }
            // Due Minus
            retailer.DueAmount -= paymentCollection.Amount;

            if (retailer.DueAmount < 0)
            {
                retailer.DueAmount = 0;
            }
            await _context.PaymentCollections.AddAsync(paymentCollection);
            await _context.SaveChangesAsync();
        }
    }
}