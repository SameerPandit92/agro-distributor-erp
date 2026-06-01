using AgroERP.Application.DTOs.PaymentCollection;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class PaymentCollectionService : IPaymentCollectionService
    {
        private readonly IPaymentCollectionRepository _repository;

        public PaymentCollectionService(IPaymentCollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync( CreatePaymentCollectionDto dto)
        {
            var payment = new PaymentCollection
                          {
                              RetailerId = dto.RetailerId,
                              Amount = dto.Amount,
                              Remarks = dto.Remarks
                          };
            await _repository.AddAsync(payment);
        }
    }
}
