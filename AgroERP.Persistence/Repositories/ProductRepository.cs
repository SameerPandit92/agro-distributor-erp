using AgroERP.Application.DTOs.PaginationFilter;
using AgroERP.Application.DTOs.Product;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using AgroERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        //public async Task<List<Product>> GetPagedAsync(PaginationFilterDto dto)
        //{
        //    var query = _context.Products.AsQueryable();
        //    // Search
        //    if (!string.IsNullOrEmpty(dto.Search))
        //    {
        //        query = query.Where(x => x.ProductName.Contains(dto.Search)|| x.CompanyName.Contains(dto.Search)|| x.Category.Contains(dto.Search));
        //    }

        //    // Sorting
        //    if (!string.IsNullOrEmpty(dto.SortBy))
        //    {
        //        switch (dto.SortBy.ToLower())
        //        {
        //            case "price":
        //                query = dto.SortOrder?.ToLower() == "desc"? query.OrderByDescending(x => x.Price): query.OrderBy(x => x.Price);
        //                break;
        //            case "productname":
        //                query = dto.SortOrder?.ToLower() == "desc"? query.OrderByDescending(x => x.ProductName): query.OrderBy(x => x.ProductName);
        //                break;
        //        }
        //    }
        //    // Pagination
        //    return await query.Skip((dto.Page - 1) * dto.PageSize).Take(dto.PageSize).ToListAsync();
        //}
        public async Task<List<Product>> GetPagedAsync(PaginationFilterDto dto)
        {
            var query = _context.Products.AsQueryable();

            // 1. Search Logic
            if (!string.IsNullOrEmpty(dto.Search))
            {
                query = query.Where(x => x.ProductName.Contains(dto.Search)
                                      || x.CompanyName.Contains(dto.Search)
                                      || x.Category.Contains(dto.Search));
            }

            // 2. Sorting Logic
            if (!string.IsNullOrEmpty(dto.SortBy))
            {
                switch (dto.SortBy.ToLower())
                {
                    case "price":
                        query = dto.SortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(x => x.Price)
                            : query.OrderBy(x => x.Price);
                        break;
                    case "productname":
                        query = dto.SortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(x => x.ProductName)
                            : query.OrderBy(x => x.ProductName);
                        break;
                }
            }

            // 3. Pagination Fallback (ये आपकी एरर को रोकेगा)
            int page = dto.Page <= 0 ? 1 : dto.Page;          // अगर Page 0 या छोटा है, तो Default 1 कर दो
            int pageSize = dto.PageSize <= 0 ? 10 : dto.PageSize; // अगर PageSize 0 या छोटा है, तो Default 10 कर दो

            // अब यह कैलकुलेशन कभी भी माइनस (-) में नहीं जाएगी
            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }



    }
}
