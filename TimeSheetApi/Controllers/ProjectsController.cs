using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Models;

namespace TimeSheetApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(TimeSheetContext context) : ControllerBase
{
    private readonly TimeSheetContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> Get() =>
        await _context.Projects.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Project>> Post(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }
}
