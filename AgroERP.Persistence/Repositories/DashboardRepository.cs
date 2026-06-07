using AgroERP.Application.DTOs.Dashboard;
using AgroERP.Application.Interfaces;
using AgroERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Persistence.Repositories
{
    public class DashboardRepository :IDashboardRepository
    {
         private readonly ApplicationDbContext _context;
     
        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
     
        public async
        Task<DashboardDto> GetDashboardAsync()
        {
            var today = DateTime.Today;
     
            return new DashboardDto
            {
                TotalRetailers = await _context.Retailers.CountAsync(),
                TotalProducts = await _context.Products.CountAsync(),
                TotalDueAmount =await _context.Retailers.SumAsync(x =>x.DueAmount),
                TodayCollection =await _context.PaymentCollections.Where(x => x.CollectionDate.Date == today).SumAsync(x =>x.Amount),
                TodaySale =await _context.Sales.Where(x => x.SaleDate.Date == today).SumAsync(x =>x.TotalAmount),
                TotalStaff =await _context.Staffs.CountAsync()
            };
        }
    }
}
