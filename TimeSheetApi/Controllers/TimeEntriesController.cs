using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Models;

namespace TimeSheetApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeEntriesController(TimeSheetContext context) : ControllerBase
{
    private readonly TimeSheetContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeEntry>>> Get()
    {
        return await _context.TimeEntries
            .Include(t => t.Employee)
            .Include(t => t.Project)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<TimeEntry>> Post(TimeEntry entry)
    {
        _context.TimeEntries.Add(entry);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
    }
}
