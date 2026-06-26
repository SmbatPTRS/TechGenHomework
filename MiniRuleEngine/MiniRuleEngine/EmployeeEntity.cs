namespace MiniRuleEngine;

public class EmployeeEntity : IEntity
{
    public int Id { get; set; }
    public string EntityType => "Employee";
    public string FullName { get; set; }
    public decimal Salary { get; set; }
    public int VacationDays { get; set; }

    public EmployeeEntity(int id, string fullName, decimal salary, int vacationDays)
    {
        Id = id;
        FullName = fullName;
        Salary = salary;
        VacationDays = vacationDays;
    }
}