using Microsoft.EntityFrameworkCore;
using TimeSheetApi.Controllers;
using TimeSheetApi.Models;

namespace TimeSheetApi.Tests;

public class CustomersControllerTests
{
    [Fact]
    public async Task Get_Returns_All_Customers()
    {
        var options = new DbContextOptionsBuilder<TimeSheetContext>()
            .UseInMemoryDatabase(databaseName: "CustomersTestDb")
            .Options;

        using var context = new TimeSheetContext(options);
        context.Customers.Add(new Customer { Id = 1, Name = "Acme" });
        await context.SaveChangesAsync();

        var controller = new CustomersController(context);
        var result = await controller.Get();

        Assert.Single(result.Value!);
        Assert.Equal("Acme", result.Value!.First().Name);
    }
}
