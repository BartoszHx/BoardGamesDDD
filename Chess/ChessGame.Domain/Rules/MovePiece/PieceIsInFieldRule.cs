using ChessGame.Domain.Chesses;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Rules.MovePiece
{
    public class PieceIsInFieldRule : IBusinessRule
    {
        public string Message => "No piece in field";

        private readonly Piece? _piece;

        public PieceIsInFieldRule(Piece? piece)
        {
            _piece = piece;
        }

        public bool IsBroken() => _piece == null;
    }
}
