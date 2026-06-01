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
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            var retailer = await _context.Retailers.FirstOrDefaultAsync(x => x.Id == sale.RetailerId);
            if (retailer == null) throw new Exception("Retailer not found");
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == sale.ProductId);
            if (product == null) throw new Exception("Product not found");
            if (product.StockQuantity < sale.Quantity)
            {
                throw new Exception("Not enough stock");
            }
            sale.TotalAmount = sale.Quantity * sale.Price;
            // stock minus
            product.StockQuantity -= (int)sale.Quantity;
            // due amount increase
            if (sale.IsCredit)
            {
                retailer.DueAmount += sale.TotalAmount;
            }
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }
    }
}
