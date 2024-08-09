using ChessGame.Domain.Boards;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Movements
{
    internal static class DoMove
    {
        public static void MovePieceOnBoard(Board board, ChessStatus status, MovementTo movement, PieceType? promotionPieceType = null)
        {
            if (movement.Movement == MovementType.Normal)
            {
                board.MovePieceToField(movement.FromPosition, movement.ToPosition);
            }

            if (movement.Movement == MovementType.Promotion)
            {
                board.MovePieceToField(movement.FromPosition, movement.ToPosition);
                board.ChangePiece(movement.ToPosition, promotionPieceType.Value);
            }

            if (movement.Movement == MovementType.EnPassant)
            {
                board.MovePieceToField(movement.FromPosition, movement.ToPosition);
                board.RemovePiece(status.PawnDoDoubleMove.Value);
            }

            if (movement.Movement == MovementType.Castling)
            {
                CastlingMovement.DoMove(board, movement);
            }
        }
    }
}
