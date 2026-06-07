using AgroERP.Application.DTOs.Salary;
using AgroERP.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStaffAttendanceRepository _attendanceRepository;

        public SalaryService(IStaffRepository staffRepository, IStaffAttendanceRepository attendanceRepository)
        {
            _staffRepository = staffRepository;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<StaffSalaryDto> CalculateSalaryAsync(int staffId, int month, int year)
        {
            var staff = (await _staffRepository.GetAllAsync()).FirstOrDefault(x => x.Id == staffId);

            if (staff == null)
            {
                throw new Exception("Staff not found");
            }

            var attendance = (await _attendanceRepository.GetAllAsync())
                .Where(x => x.StaffId == staffId &&
                    x.AttendanceDate.Month == month &&
                    x.AttendanceDate.Year == year).ToList();

            int presentDays = attendance.Count(x => x.Status =="Present");
            int halfDays =attendance.Count(x => x.Status =="Half Day");
            int leaveDays =attendance.Count(x =>x.Status =="Leave");
            decimal perDaySalary = staff.Salary / 30;
            decimal finalSalary = (presentDays * perDaySalary)+ (halfDays * perDaySalary/ 2);
           return new
                     StaffSalaryDto
                      {
                          StaffId =staff.Id,               
                          StaffName =staff.FullName,                 
                          MonthlySalary =staff.Salary,               
                          PresentDays = presentDays,      
                          HalfDays = halfDays,
                          LeaveDays =leaveDays,
                          FinalSalary =finalSalary
                      };
        }
    }
}
