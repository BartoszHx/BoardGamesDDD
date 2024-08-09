using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Rules.Promotion
{
    public class ChoseCorrectPieceTypeToPromotionRule : IBusinessRule
    {
        public string Message => throw new NotImplementedException();

        private readonly PieceType _pieceType;
        private static PieceType[] _pieceTypesNotAllowed = new PieceType[] { PieceType.Pawn, PieceType.King };

        public ChoseCorrectPieceTypeToPromotionRule(PieceType pieceType)
        {
            _pieceType = pieceType;
        }

        public bool IsBroken() => _pieceTypesNotAllowed.Contains(_pieceType);
    }
}
