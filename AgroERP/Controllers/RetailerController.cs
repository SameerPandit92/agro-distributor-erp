using AgroERP.Application.DTOs.Retailer;
using AgroERP.Application.Interfaces;
using AgroERP.Controllers;
using AgroERP.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailerController : ControllerBase
    {
        
        private readonly IRetailerService _retailerService;
        private readonly IValidator<CreateRetailerDto>  _validator;
        public RetailerController(IRetailerService retailerService,IValidator<CreateRetailerDto> validator)
        {
            _retailerService = retailerService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var retailers = await _retailerService.GetAllAsync();
            return Ok(retailers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var retailer =await _retailerService.GetByIdAsync(id);

            if (retailer == null) return NotFound();
            return Ok(retailer);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateRetailerDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            await _retailerService.AddAsync(dto);
            return Ok("Retailer Added Successfully");
        }

        [HttpPut]
        public async Task<IActionResult>Update(UpdateRetailerDto retailer)
        {
            await _retailerService.UpdateAsync(retailer);
            return Ok("Retailer Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            await _retailerService.DeleteAsync(id);

            return Ok("Retailer Deleted Successfully");
        }
    }
}
