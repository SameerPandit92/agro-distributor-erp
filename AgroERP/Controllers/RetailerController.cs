using AgroERP.Application.Interfaces;
using AgroERP.Controllers;
using AgroERP.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailerController : ControllerBase
    {
        
    private readonly IRetailerRepository _retailerRepository;

        public RetailerController(IRetailerRepository retailerRepository)
        {
            _retailerRepository = retailerRepository;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var retailers = await _retailerRepository.GetAllAsync();
            return Ok(retailers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var retailer =await _retailerRepository.GetByIdAsync(id);

            if (retailer == null) return NotFound();
            return Ok(retailer);
        }

        [HttpPost]
        public async Task<IActionResult>Add(Retailer retailer)
        {
            await _retailerRepository.AddAsync(retailer);

            return Ok("Retailer Added Successfully");
        }

        [HttpPut]
        public async Task<IActionResult>Update(Retailer retailer)
        {
            await _retailerRepository.UpdateAsync(retailer);
            return Ok("Retailer Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            await _retailerRepository.DeleteAsync(id);

            return Ok("Retailer Deleted Successfully");
        }
    }
}
