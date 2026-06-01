using AgroERP.Application.DTOs.Sale;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task AddAsync(CreateSaleDto dto)
        {
            var sale = new Sale
            {
                RetailerId = dto.RetailerId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Price = dto.Price,
                IsCredit = dto.IsCredit
            };

            await _saleRepository.AddAsync(sale);
        }
    }
}
