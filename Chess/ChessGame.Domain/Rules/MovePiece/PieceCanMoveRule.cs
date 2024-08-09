using ChessGame.Domain.Movements;

namespace ChessGame.Domain.Rules.MovePiece
{
    internal class PieceCanMoveRule : IBusinessRule
    {
        private readonly MovementTo movment;

        public string Message => "Piece can't move to this field";

        public PieceCanMoveRule(MovementTo movment)
        {
            this.movment = movment;
        }

        public bool IsBroken() => movment == null;
    }
}
