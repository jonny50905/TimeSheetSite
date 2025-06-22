using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Controllers;
using TimeSheetApi.Models;

namespace TimeSheetApi.Tests;

public class ProjectsControllerTests
{
    [Fact]
    public async Task Get_Returns_Projects_With_Customers()
    {
        var options = new DbContextOptionsBuilder<TimeSheetContext>()
            .UseInMemoryDatabase(databaseName: "ProjectsTestDb")
            .Options;

        using var context = new TimeSheetContext(options);
        var customer = new Customer { Id = 1, Name = "Acme" };
        context.Customers.Add(customer);
        context.Projects.Add(new Project
        {
            Id = 1,
            Name = "Site",
            CustomerId = 1,
            Customer = customer,
            HourlyRate = 100,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1),
            Amount = 1000
        });
        await context.SaveChangesAsync();

        var controller = new ProjectsController(context);
        var result = await controller.Get();

        Assert.Single(result.Value!);
        Assert.Equal("Acme", result.Value!.First().Customer!.Name);
    }
}
