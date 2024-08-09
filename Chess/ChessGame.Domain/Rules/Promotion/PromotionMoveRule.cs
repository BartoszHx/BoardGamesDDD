
using ChessGame.Domain.Movements;

namespace ChessGame.Domain.Rules.Promotion
{
    public class PromotionMoveRule : IBusinessRule
    {
        public string Message => "Is not promotion move";

        private readonly MovementTo _movementTo;

        public PromotionMoveRule(MovementTo movementTo)
        {
            _movementTo = movementTo;
        }

        public bool IsBroken() => _movementTo.Movement != MovementType.Promotion;
    }
}
