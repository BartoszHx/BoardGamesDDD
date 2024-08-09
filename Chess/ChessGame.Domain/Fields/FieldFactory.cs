using ChessGame.Domain.Chesses;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Fields
{
    internal static class FieldFactory
    {
        public static Field[] CreateEmptyField()
        {
            var fields = new Field[64];

            for (int i = 0; i <= 63; i++)
            {
                var position = GetPosition(i + 1);
                var field = Field.Create(position);
                fields[i] = field;
            }

            return fields;
        }

        public static Field[] CreateChessStartField()
        {
            var fields = CreateEmptyField();

            foreach(var field in fields)
            {
                var piece = GetPiece(field.Position);
                if (piece.HasValue)
                {
                    var color = GetColor(field.Position);
                    field.PutPieceOnField(Piece.Create(piece.Value, color));
                }
            }

            return fields;
        }

        private static FieldPosition GetPosition(int i)
        {
            return (FieldPosition)i;
        }

        private static PieceType? GetPiece(FieldPosition position)
        {
            if (position.IsBetween(FieldPosition.A2, FieldPosition.H2) || position.IsBetween(FieldPosition.A7, FieldPosition.H7)) return PieceType.Pawn;
            if (position.IsIn(FieldPosition.A1, FieldPosition.H1, FieldPosition.A8, FieldPosition.H8)) return PieceType.Rock;
            if (position.IsIn(FieldPosition.B1, FieldPosition.G1, FieldPosition.B8, FieldPosition.G8)) return PieceType.Knight;
            if (position.IsIn(FieldPosition.C1, FieldPosition.F1, FieldPosition.C8, FieldPosition.F8)) return PieceType.Bishop;
            if (position.IsIn(FieldPosition.D1, FieldPosition.D8)) return PieceType.Queen;
            if (position.IsIn(FieldPosition.E1, FieldPosition.E8)) return PieceType.King;

            return null;
        }

        private static ColorType GetColor(FieldPosition position)
        {
            if (position.IsBetween(FieldPosition.A1, FieldPosition.H2)) return ColorType.White;
            if (position.IsBetween(FieldPosition.A7, FieldPosition.H8)) return ColorType.Black;
            throw new Exception("Error");
        }
    }
}
