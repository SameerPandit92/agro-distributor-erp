using AgroERP.Application.DTOs.Ledger;
using AgroERP.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class LedgerService : ILedgerService
    {
        private readonly  ILedgerRepository  _repository;

        public LedgerService(ILedgerRepository  repository)
        {
            _repository = repository;
        }

        public async
       Task<List<CustomerLedgerDto>> GetCustomerLedgerAsync(int retailerId)
        {
            var sales = await _repository.GetSalesAsync(retailerId);

            var payments = await _repository.GetPaymentsAsync(retailerId);

            var ledger =new List <CustomerLedgerDto>();

            decimal runningBalance = 0;

            foreach (var sale in sales)
            {
                runningBalance += sale.TotalAmount;

                ledger.Add( new CustomerLedgerDto
                    {
                        Type = "Sale",
                        Date = sale.SaleDate,
                        Description ="Sale Entry",
                        Debit = sale.TotalAmount,
                        Credit = 0,
                        Balance = runningBalance
                    });
            }

            foreach (var payment in payments)
            {
                runningBalance -= payment.Amount;

                ledger.Add( new CustomerLedgerDto
                    {
                        Type = "Payment",
                        Date = payment.CollectionDate,
                        Description = payment.Remarks,
                        Debit = 0,
                        Credit = payment.Amount,
                        Balance = runningBalance
                    });
            }
            return ledger.OrderBy(x => x.Date).ToList();
        }
    }
}
