using AgroERP.Application.DTOs.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface ISalaryService
    {
        Task<StaffSalaryDto> CalculateSalaryAsync(int staffId,int month,int year);
    }
}
