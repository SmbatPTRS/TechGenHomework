namespace MiniRuleEngine;

public delegate void RuleCheck(IEntity entity);

public class Rule
{
        public  string Name { get; }
        public string TargetEntityType { get; }
        public  RuleCheck Check { get; }

        public bool AppliesTo(IEntity entity)
        {
                return TargetEntityType == entity.EntityType;
        }

        public Rule(string name, string targetEntityType, RuleCheck check)
        {
                Name = name;
                TargetEntityType = targetEntityType;
                Check = check;
        }
        
}