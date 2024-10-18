using DOTNETRELATIONS.Data.Entities;
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
}