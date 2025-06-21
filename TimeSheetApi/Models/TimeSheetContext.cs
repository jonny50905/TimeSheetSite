using Microsoft.EntityFrameworkCore;

namespace TimeSheetApi.Models;

public class TimeSheetContext(DbContextOptions<TimeSheetContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();
    public DbSet<Customer> Customers => Set<Customer>();
}
