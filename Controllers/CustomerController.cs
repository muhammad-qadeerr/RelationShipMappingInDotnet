using AutoMapper;
using DOTNETRELATIONS.Data.Entities;
using DOTNETRELATIONS.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOTNETRELATIONS.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{ 
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CustomerController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    // GET Endpoint Action Methods
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _appDbContext.Customer
        .Include(ca => ca.CustomerAddresses)
        .ToListAsync();
        return Ok(customers);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customers = await _appDbContext.Customer
        .Include(ca => ca.CustomerAddresses)
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync();
        return Ok(customers);
    }

    // Manual Mapper for mapping data to avoid cyclic references for entities using DTOs.
    // Commenting this method as now we have configured an auto-mapper.
    /*private Customer MapCustomer(CustomerDto payload)
    {
        var result = new Customer
        {
            FirstName = payload.FirstName,
            LastName = payload.LastName,
            Phone = payload.Phone,
            CustomerAddresses = new List<CustomerAddresses>()
        };
        payload.CustomerAddresses?.ForEach(_ => {
            var newAddress = new CustomerAddresses();
            newAddress.City = _.City;
            newAddress.Country = _.Country;
            result.CustomerAddresses.Add(newAddress);
        });
        return result;
    }*/

    // POST Endpoint Action Methods
    [HttpPost]
    public async Task<IActionResult> AddCustomer(CustomerDto customerPayload)
    {
        //var newCustomer = MapCustomer(customerPayload);
        var newCustomer = _mapper.Map<Customer>(customerPayload);
        await _appDbContext.Customer.AddAsync(newCustomer);
        await _appDbContext.SaveChangesAsync();
        return Created($"/customer/{newCustomer.Id}", newCustomer);
    }
    
     // PUT Endpoint Action Methods
    [HttpPut]
    public async Task<IActionResult> UpdateCustomer(CustomerDto customerPayload)
    {
        var updatedCustomer = _mapper.Map<Customer>(customerPayload);
        _appDbContext.Customer.Update(updatedCustomer);
        await _appDbContext.SaveChangesAsync();
        return Ok(updatedCustomer);
    }

    [HttpDelete]
    [Route("{id:int}")]
     public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customerToDelete = await _appDbContext
         .Customer.Include(_ => _.CustomerAddresses)
         .Where(_ => _.Id == id)
         .FirstOrDefaultAsync();
        if(customerToDelete == null)
        {
            return NotFound();
        }
        _appDbContext.Customer.Remove(customerToDelete);
        await _appDbContext.SaveChangesAsync();
        return NoContent();
    }
}