using AgroERP.Application.DTOs.Product;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IProductService
    {
        Task AddAsync(CreateProductDto dto);

        Task<List<Product>> GetAllAsync();
    }
}
