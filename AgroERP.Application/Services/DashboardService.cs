using AgroERP.Application.DTOs.Dashboard;
using AgroERP.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            return await _repository.GetDashboardAsync();
        }
    }
}
