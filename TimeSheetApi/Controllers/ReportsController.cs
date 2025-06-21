using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Models;

namespace TimeSheetApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController(TimeSheetContext context) : ControllerBase
{
    private readonly TimeSheetContext _context = context;

    public record ProjectReport(int ProjectId, string ProjectName, decimal Hours, decimal Cost, decimal Revenue, decimal Profit);

    [HttpGet("projects")]
    public async Task<ActionResult<IEnumerable<ProjectReport>>> GetProjectReports()
    {
        var reports = await _context.TimeEntries
            .Include(t => t.Employee)
            .Include(t => t.Project)
            .GroupBy(t => new { t.ProjectId, t.Project!.Name, t.Project.HourlyRate })
            .Select(g => new ProjectReport(
                g.Key.ProjectId,
                g.Key.Name,
                g.Sum(x => x.Hours),
                g.Sum(x => x.Hours * x.Employee!.HourlyRate),
                g.Sum(x => x.Hours * g.Key.HourlyRate),
                g.Sum(x => x.Hours * g.Key.HourlyRate) - g.Sum(x => x.Hours * x.Employee!.HourlyRate)
            ))
            .ToListAsync();

        return reports;
    }
}
