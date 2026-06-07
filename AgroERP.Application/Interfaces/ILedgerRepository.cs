using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface ILedgerRepository
    {
        Task<List<Sale>>
        GetSalesAsync(int retailerId);
        Task<List<PaymentCollection>>GetPaymentsAsync(int retailerId);
    }
}
