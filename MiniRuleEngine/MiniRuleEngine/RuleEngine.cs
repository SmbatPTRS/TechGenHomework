namespace MiniRuleEngine;

public class RuleEngine
{
    protected Rule[] _rules;
    
    private int capacity=4;
    private int _count = 0;
    public RuleEngine(int Capacity)
    {
        if(Capacity<4){
            Capacity = capacity;
        }
        _rules = new Rule[Capacity];
        capacity = Capacity;
    }

    public void  AddRule(Rule rule)
    {
        if (_count == _rules.Length)
        {
            Array.Resize(ref _rules, capacity*2);
        }
        _rules[_count] = rule;
        _count++;
    }

    public void ValidateFailFast(IEntity entity)
    {
        foreach (var rule in _rules)
        {
            if(!rule.AppliesTo(entity)){continue;}

            try
            {
                rule.Check(entity);
            }
            catch (RuleViolationException ex)
            {
                throw;

            }
            catch (Exception ex)
            {
                throw new RuleViolationException(rule.Name, $"Unexpected rule error:{ex.Message}");
            }
        }
    }

    public void ValidateCollectAll(IEntity entity)
    {
        var violationException = new List<RuleViolationException>();
        
        for (int i = 0; i < _count; i++)
        {
            Rule rule = _rules[i];
            if (!rule.AppliesTo(entity))
            {
                continue;
            }

            try
            {
                rule.Check(entity);
            }
            catch (RuleViolationException ex)
            {
                violationException.Add(ex);
            }
            catch (Exception ex)
            {
                violationException.Add(new RuleViolationException(rule.Name, $"Unexpected rule error: {ex.Message}"));
            }
        }

        if (violationException.Count > 0)
        {
            throw new EntityViolationException(entity, violationException.ToArray());
        }
    }
}