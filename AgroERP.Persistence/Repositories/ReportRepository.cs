using AgroERP.Application.DTOs.Reports;
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
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository( ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <List<MonthlyCollectionReportDto>> GetMonthlyCollectionAsync(int month,int year)
        {
            return await _context.PaymentCollections.Include(x => x.Retailer)
                .Where(x => x.CollectionDate.Month == month && x.CollectionDate.Year == year)
                .Select(x => new  MonthlyCollectionReportDto
                    {
                        RetailerName = x.Retailer!.ShopName,
                        Amount =x.Amount,
                        CollectionDate = x.CollectionDate,
                        Remarks = x.Remarks
                    })
                .ToListAsync();
        }
    }
}
