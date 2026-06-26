namespace MiniRuleEngine;

class Program
{
    static void Main(string[] args)
    {
        RuleEngine engine = new RuleEngine(4);
        RuleRegistration.RegisterRules(engine);
        IEntity emplyee1 = new EmployeeEntity(1,"",60,10);
        IEntity emplyee2 = new EmployeeEntity(2,"",0,10);
        IEntity order1 = new OrderEntity(20,"John Doe",10);
        IEntity order2 = new OrderEntity(30,"",10);
        IEntity[] entites = new IEntity[4];
        entites[0] = emplyee1;
        entites[1] = emplyee2;
        entites[2] = order1;
        entites[3] = order2;
        

        Console.WriteLine("==== Collect-ALL Mode===== ");
        foreach (IEntity entity in entites)
        {
            try
            {
                engine.ValidateCollectAll(entity);
                Console.WriteLine($"{entity.EntityType} #{entity.Id} — VALID");
            }
            catch (EntityViolationException ex)
            {
                Console.WriteLine(ex.Message); // "Employee #1 has 1 validation error(s)."
                foreach (RuleViolationException violation in ex.Violations)
                {
                    Console.WriteLine($"  - [{violation.RuleName}]: {violation.Message}");
                }
            }
            Console.WriteLine();
        }
    }
}