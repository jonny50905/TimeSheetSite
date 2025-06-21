namespace TimeSheetApi.Models;

public class TimeEntry
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public DateTime Date { get; set; }
    public decimal Hours { get; set; }
}
