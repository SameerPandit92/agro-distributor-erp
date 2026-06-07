using AgroERP.Application.DTOs.Reports;
using AgroERP.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository  _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<MonthlyCollectionReportDto>> GetMonthlyCollectionAsync(int month,int year)
        {
            return await _repository.GetMonthlyCollectionAsync(month,year);
        }
    }
}