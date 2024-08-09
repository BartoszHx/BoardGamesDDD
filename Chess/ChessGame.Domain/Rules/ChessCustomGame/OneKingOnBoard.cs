using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses;

namespace ChessGame.Domain.Rules.ChessCustomBoard
{
    internal class OneKingOnBoard : IBusinessRule
    {
        private readonly ColorType kingColor;
        private readonly FieldBuilder[] fields;

        public string Message => $"Only one {kingColor} king can be on board";

        public OneKingOnBoard(ColorType kingColor, FieldBuilder[] fields)
        {
            this.kingColor = kingColor;
            this.fields = fields;
        }

        public bool IsBroken() => fields.Where(a => a.IsPiece(Pieces.PieceType.King, kingColor)).Count() != 1;
    }
}
