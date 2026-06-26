namespace MiniRuleEngine;

public class RuleViolationException : Exception
{
    public string RuleName { get;}
    public RuleViolationException(string ruleName, string message) :  base(message)
    {
        RuleName = ruleName;
    }
}