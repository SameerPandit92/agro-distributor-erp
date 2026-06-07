using AgroERP.Application.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IReportRepository
    {
        Task<List <MonthlyCollectionReportDto>> GetMonthlyCollectionAsync( int month,int year);
    }
}
