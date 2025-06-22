using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Controllers;
using TimeSheetApi.Models;

namespace TimeSheetApi.Tests;

public class TimeEntriesControllerTests
{
    [Fact]
    public async Task Get_Returns_TimeEntries_With_Relationships()
    {
        var options = new DbContextOptionsBuilder<TimeSheetContext>()
            .UseInMemoryDatabase(databaseName: "TimeEntriesTestDb")
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
        context.TimeEntries.Add(new TimeEntry
        {
            Id = 1,
            EmployeeId = 1,
            Employee = employee,
            ProjectId = 1,
            Project = project,
            Date = DateTime.Today,
            Hours = 5,
            Notes = "work"
        });
        await context.SaveChangesAsync();

        var controller = new TimeEntriesController(context);
        var result = await controller.Get();

        Assert.Single(result.Value!);
        var entry = result.Value!.First();
        Assert.Equal("John", entry.Employee!.Name);
        Assert.Equal("Site", entry.Project!.Name);
    }
}
