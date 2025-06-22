using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Controllers;
using TimeSheetApi.Models;

namespace TimeSheetApi.Tests;

public class EmployeesControllerTests
{
    [Fact]
    public async Task Get_Returns_All_Employees()
    {
        var options = new DbContextOptionsBuilder<TimeSheetContext>()
            .UseInMemoryDatabase(databaseName: "EmployeesTestDb")
            .Options;

        using var context = new TimeSheetContext(options);
        context.Employees.Add(new Employee { Id = 1, Name = "John", HourlyRate = 50, MonthlySalary = 8000 });
        await context.SaveChangesAsync();

        var controller = new EmployeesController(context);
        var result = await controller.Get();

        Assert.Single(result.Value!);
        Assert.Equal("John", result.Value!.First().Name);
    }
}
