using AgroERP.Application.DTOs.Retailer;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class RetailerService: IRetailerService
    {
       private readonly IRetailerRepository  _retailerRepository;

        public RetailerService( IRetailerRepository retailerRepository)
        {
            _retailerRepository = retailerRepository;
        }

        public async Task<List<RetailerDto>>GetAllAsync()
        {
            var retailers =await _retailerRepository.GetAllAsync();

            return retailers.Select(x => new RetailerDto
                      {
                          Id = x.Id,
                          ShopName = x.ShopName,
                          OwnerName = x.OwnerName,
                          MobileNumber =x.MobileNumber,
                          Address = x.Address,
                          CreditLimit =x.CreditLimit,
                          DueAmount = x.DueAmount
                      }).ToList();
        }

        public async Task<RetailerDto?> GetByIdAsync(int id)
        {
            var retailer = await _retailerRepository.GetByIdAsync(id);

            if (retailer == null) return null;
            return new RetailerDto
            {
                Id = retailer.Id,
                ShopName = retailer.ShopName,
                OwnerName = retailer.OwnerName,
                MobileNumber = retailer.MobileNumber,
                Address = retailer.Address,
                CreditLimit = retailer.CreditLimit,
                DueAmount = retailer.DueAmount
            };
        }

        public async Task AddAsync(CreateRetailerDto dto)
        {
            var retailer = new Retailer
                           {
                               ShopName = dto.ShopName,
                               OwnerName = dto.OwnerName,
                               MobileNumber = dto.MobileNumber,
                               Address = dto.Address,
                               CreditLimit = dto.CreditLimit,
                               DueAmount = dto.DueAmount
                           };
            await _retailerRepository.AddAsync(retailer);
        }

        public async Task UpdateAsync(UpdateRetailerDto dto)
        {
            var retailer =new Retailer
                          {
                              Id = dto.Id,
                              ShopName =dto.ShopName,
                              OwnerName =dto.OwnerName,
                              MobileNumber =dto.MobileNumber,
                              Address = dto.Address,
                              CreditLimit = dto.CreditLimit,
                              DueAmount = dto.DueAmount
                          };
            await _retailerRepository.UpdateAsync(retailer);
        }

        public async Task DeleteAsync(int id)
        {
            await _retailerRepository.DeleteAsync(id);
        }
    }
}
