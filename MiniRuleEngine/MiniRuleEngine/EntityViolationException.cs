namespace MiniRuleEngine;

public class EntityViolationException : Exception
{
    public IEntity Entity { get; }
    public RuleViolationException [] Violations { get; }

    public EntityViolationException(IEntity entity, RuleViolationException[] violations) : base($"{entity.EntityType} #{entity.Id} has {violations.Length} validation error(s).")
    {
        Entity = entity;
        Violations = violations;
    }
    
}