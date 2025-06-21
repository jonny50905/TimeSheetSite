using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Models;

namespace TimeSheetApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(TimeSheetContext context) : ControllerBase
{
    private readonly TimeSheetContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> Get() =>
        await _context.Employees.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Employee>> Post(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
    }
}
