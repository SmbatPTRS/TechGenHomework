namespace MiniRuleEngine;

public static class RuleRegistration
{
    public static void RegisterRules(RuleEngine engine)
    {
        engine.AddRule(new Rule("Employee must have a name", "Employee",
            delegate(IEntity entity)
            {
                EmployeeEntity employee = (EmployeeEntity)entity;
                if (string.IsNullOrWhiteSpace(employee.FullName))
                {
                    throw new RuleViolationException("Employee must have a full name",
                        "Employee does not have a full name");
                }
            }
        ));

        engine.AddRule(new Rule("SalaryBounds", "Employee",
            delegate(IEntity entity)
            {
                EmployeeEntity employee = (EmployeeEntity)entity;
                if (employee.Salary < 0 || employee.Salary > 1000000)
                {
                    throw new RuleViolationException("Salary Bonuds", "Salary must be between 0 and 1000000");
                }
            }
        ));

        engine.AddRule(new Rule("OrderMustHaveAName", "Order", delegate(IEntity entity)
        {
            OrderEntity order = (OrderEntity)entity;
            if (string.IsNullOrWhiteSpace(order.CustomerName))
            {
                throw new RuleViolationException("OrderMustHaveAName", "Order does not have a name");
            }
        }));
    }
}          