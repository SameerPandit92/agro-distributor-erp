using AgroERP.Application.DTOs.PaginationFilter;
using AgroERP.Application.DTOs.Product;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetPagedAsync( PaginationFilterDto dto);
    }
}
