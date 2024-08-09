using ChessGame.Domain.Boards;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;

namespace ChessGame.Domain.Movements
{
    internal static class KingMovement
    {
        public static List<MovementTo> Movement(ColorType color, FieldPosition kingPosition, Board board, ChessStatus status)
        {
            var movements = new List<MovementTo>();

            Normal(movements, kingPosition);
            Castling(movements, color, board, status);

            return movements;
        }

        private static void Normal(List<MovementTo> movements, FieldPosition kingPosition)
        {
            movements.AddMovement(kingPosition, kingPosition.TakeUp());
            movements.AddMovement(kingPosition, kingPosition.TakeUpLeft());
            movements.AddMovement(kingPosition, kingPosition.TakeUpRight());
            movements.AddMovement(kingPosition, kingPosition.TakeLeft());
            movements.AddMovement(kingPosition, kingPosition.TakeRight());
            movements.AddMovement(kingPosition, kingPosition.TakeDown());
            movements.AddMovement(kingPosition, kingPosition.TakeDownLeft());
            movements.AddMovement(kingPosition, kingPosition.TakeDownRight());
        }

        private static void Castling(List<MovementTo> movements, ColorType color, Board board, ChessStatus status)
        {
            movements.AddRange(CastlingMovement.Movements(color, board, status));
        }
    }
}
