using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IPaymentCollectionRepository
    {
        Task AddAsync(PaymentCollection paymentCollection);
    }
}
