using ChessGame.Domain.Boards;
using ChessGame.Domain.Movements;

namespace ChessGame.Domain.Chesses
{
    internal static class CheckVerification
    {
        public static void RemoveCheckMoves(Board board, List<MovementTo> movements, ChessStatus status)
        {
            var movementsToDelete = new List<MovementTo>();

            foreach (var movementTo in movements)
            {
                var copyBoard = board.Copy();
                var copyStatus = status.Copy();

                if (IsCheck(movementTo, copyBoard, copyStatus))
                {
                    movementsToDelete.Add(movementTo);
                }
            }

            foreach (var toDelete in movementsToDelete)
            {
                movements.Remove(toDelete);
            }
        }

        private static bool IsCheck(MovementTo movementTo, Board board, ChessStatus status)
        {
            var playerTurn = status.PlayerTurn;
            var piece = board.TakePieceInformation(movementTo.FromPosition);

            DoMove.MovePieceOnBoard(board, status, movementTo);
            var copyPoolMovementTo = PoolMovement.Create(board, status, false);

            var kingPosition = board.GetKingPosition(playerTurn);

            var poolMovementAfterPieceMove = copyPoolMovementTo.ShowMovements();
            var isCheck = poolMovementAfterPieceMove.Any(a => a.ToPosition == kingPosition);

            return isCheck;
        }
    }
}
