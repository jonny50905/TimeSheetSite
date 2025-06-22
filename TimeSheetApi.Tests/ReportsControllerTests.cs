using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Controllers;
using TimeSheetApi.Models;

namespace TimeSheetApi.Tests;

public class ReportsControllerTests
{
    [Fact]
    public async Task GetProjectReports_Returns_Correct_Aggregates()
    {
        var options = new DbContextOptionsBuilder<TimeSheetContext>()
            .UseInMemoryDatabase(databaseName: "ReportsTestDb")
            .Options;

        using var context = new TimeSheetContext(options);
        var employee = new Employee { Id = 1, Name = "John", HourlyRate = 50, MonthlySalary = 8000 };
        var customer = new Customer { Id = 1, Name = "Acme" };
        var project = new Project
        {
            Id = 1,
            Name = "Site",
            CustomerId = 1,
            Customer = customer,
            HourlyRate = 100,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1),
            Amount = 1000
        };
        context.Employees.Add(employee);
        context.Customers.Add(customer);
        context.Projects.Add(project);
        context.TimeEntries.AddRange(
            new TimeEntry { Id = 1, Employee = employee, EmployeeId = 1, Project = project, ProjectId = 1, Date = DateTime.Today, Hours = 2 },
            new TimeEntry { Id = 2, Employee = employee, EmployeeId = 1, Project = project, ProjectId = 1, Date = DateTime.Today, Hours = 3 }
        );
        await context.SaveChangesAsync();

        var controller = new ReportsController(context);
        var result = await controller.GetProjectReports();
        var report = Assert.Single(result.Value!);

        Assert.Equal(1, report.ProjectId);
        Assert.Equal(5, report.Hours);
        Assert.Equal(250, report.Cost);
        Assert.Equal(500, report.Revenue);
        Assert.Equal(250, report.Profit);
    }
}
