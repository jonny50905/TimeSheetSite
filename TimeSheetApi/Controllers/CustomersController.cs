using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Models;

namespace TimeSheetApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(TimeSheetContext context) : ControllerBase
{
    private readonly TimeSheetContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get() =>
        await _context.Customers.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Customer>> Post(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
    }
}
