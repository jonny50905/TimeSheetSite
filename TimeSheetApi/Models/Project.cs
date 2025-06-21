namespace TimeSheetApi.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
}
