using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Builders
{
    public record FieldBuilder
    {
        public FieldPosition Position { get; init; }

        public PieceBuilder Piece { get; set; }

        private FieldBuilder(FieldPosition position, PieceBuilder piece)
        {
            Position = position;
            Piece = piece;
        }

        public bool IsPiece(PieceType type, ColorType color) => Piece != null && Piece.Type == type && Piece.Color == color;

        public static FieldBuilder Create(FieldPosition position) => new FieldBuilder(position, null);

        public static FieldBuilder[] CreateStandardFields()
        {
            var fields = new FieldBuilder[64];

            for (int i = 0; i <= 63; i++)
            {
                var position = GetPosition(i + 1);
                var field = FieldBuilder.Create(position);
                fields[i] = field;
            }

            return fields;
        }

        private static FieldPosition GetPosition(int i)
        {
            return (FieldPosition)i;
        }
    }
}
