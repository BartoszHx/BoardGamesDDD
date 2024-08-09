using ChessGame.Domain.Movements;

namespace ChessGame.Domain.Rules.MovePiece
{
    internal class NotPromotionMoveRule : IBusinessRule
    {
        public string Message => "Is promotion move. Use MovePiecePromotion method.";

        private readonly MovementTo _movementTo;

        public NotPromotionMoveRule(MovementTo movement)
        {
            _movementTo = movement;
        }

        public bool IsBroken() => _movementTo.Movement == MovementType.Promotion;
    }
}
