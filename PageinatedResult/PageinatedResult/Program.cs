using PageinatedResult.PagedQuery;

namespace PageinatedResult;

class Program
{
    static void Main(string[] args)
    {
        
        var employees = new List<Employee>
        {
            new() { Name = "Ann",   Department = "HR", Salary = 3000 },
            new() { Name = "Bob",   Department = "IT", Salary = 5000 },
            new() { Name = "Carla", Department = "HR", Salary = 4000 },
        };

        var options = new QueryOptions<Employee, decimal>
        {
            filterRule = e => e.Department == "HR",
            keyReturner = e => e.Salary,
            descending =  true,
            page = 1,
            pageSize = 2
        };

        var result = QueryExecutor<Employee,decimal>.Execute(employees, options);

        foreach (var emp in result.Items)
            Console.WriteLine($"{emp.Name} - {emp.Salary}");

        Console.WriteLine($"Page {result.page}/{result.totalPages}, Total: {result.totalCount}");

    }
    
}




