using AgroERP.Application.DTOs.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface ILedgerService
    {
        Task<List<CustomerLedgerDto>> GetCustomerLedgerAsync(int retailerId);
    }
}
