using AgroERP.Application.DTOs.Product;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository  _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                CompanyName = dto.CompanyName,
                Category = dto.Category,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };
            await _repository.AddAsync(product);
        }

        public async  Task<List<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}