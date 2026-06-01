using AgroERP.Application.DTOs.PaymentCollection;
using System;


namespace AgroERP.Application.Interfaces
{
    public interface IPaymentCollectionService
    {
        public Task AddAsync(CreatePaymentCollectionDto dto);
    }
}
