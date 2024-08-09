using ChessGame.Domain.Chesses;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Rules.MovePiece
{
    internal class PlayerTurnRule : IBusinessRule
    {
        public string Message => "Is not player turn.";

        private readonly ColorType _playerTurn;
        private readonly Piece _piece;

        public PlayerTurnRule(ColorType playerTurn, Piece piece)
        {
            _playerTurn = playerTurn;
            _piece = piece;
        }

        public bool IsBroken() => _playerTurn != _piece.Color;
    }
}
