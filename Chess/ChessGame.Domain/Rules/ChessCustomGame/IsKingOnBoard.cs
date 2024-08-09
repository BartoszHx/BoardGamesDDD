using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;

namespace ChessGame.Domain.Rules.ChessCustomBoard
{
    internal class IsKingOnBoard : IBusinessRule
    {
        private readonly ColorType kingColor;
        private readonly FieldBuilder[] fields;

        public string Message => $"{kingColor} king isn't on board";

        public IsKingOnBoard(ColorType kingColor, FieldBuilder[] fields)
        {
            this.kingColor = kingColor;
            this.fields = fields;
        }

        public bool IsBroken() => !fields.Any(a => a.IsPiece(Pieces.PieceType.King, kingColor));
    }
}
