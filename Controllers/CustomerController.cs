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
    public CustomerController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

    }

    // GET Endpoint Action Methods
    [HttpGet]
    //[Route("Customer", Name = "GetAllCustomers")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _appDbContext.Customer
        .Include(ca => ca.CustomerAddresses)
        .ToListAsync();
        return Ok(customers);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customers = await _appDbContext.Customer
        .Include(ca => ca.CustomerAddresses)
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync();
        return Ok(customers);
    }

    private Customer MapCustomer(CustomerDto payload)
    {
        var result = new Customer();
        result.FirstName = payload.FirstName;
        result.LastName = payload.LastName;
        result.Phone = payload.Phone;
        result.CustomerAddresses = new List<CustomerAddresses>();
        payload.CustomerAddresses.ForEach(_ => {
            var newAddress = new CustomerAddresses();
            newAddress.City = _.City;
            newAddress.Country = _.Country;
            result.CustomerAddresses.Add(newAddress);
        });
        return result;
    }
    [HttpPost]
    public async Task<IActionResult> AddCustomer(CustomerDto customerPayload)
    {
        var newCustomer = MapCustomer(customerPayload);
        await _appDbContext.Customer.AddAsync(newCustomer);
        await _appDbContext.SaveChangesAsync();
        return Created($"/customer/{newCustomer.Id}", newCustomer);
    }
}