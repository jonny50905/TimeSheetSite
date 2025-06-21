namespace TimeSheetApi.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public decimal MonthlySalary { get; set; }
}
