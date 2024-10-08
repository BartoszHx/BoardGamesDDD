﻿namespace ChessGame.Domain
{
    public class BusinessRuleValidationException : Exception
    {
        public IBusinessRule BrokenRule { get; }

        public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
        }

        public override string ToString()
        {
            return BrokenRule.Message;
        }
    }
}
