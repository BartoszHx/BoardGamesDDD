using ChessGame.Infrastructure.Common;

namespace ChessGame.Domain
{
    public abstract class EntityAggregateRoot : BaseAggregateRoot
    {
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
