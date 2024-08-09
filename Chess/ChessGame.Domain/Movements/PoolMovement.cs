using ChessGame.Domain.Boards;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Dtos;
using ChessGame.Domain.Fields;

namespace ChessGame.Domain.Movements
{
    internal class PoolMovement
    {
        private List<MovementTo> movementsToList;

        private PoolMovement()
        {
            movementsToList = new List<MovementTo>();
        }

        public static PoolMovement Create(Board board, ChessStatus status, bool doCheckVerification = true)
        {
            var poolMovement = new PoolMovement();
            poolMovement.GetPieceMovementForPlayer(board, status, status.PlayerTurn, doCheckVerification);
            return poolMovement;
        }

        public static PoolMovement Update(Board board, ChessStatus status) => Create(board, status);

        public static PoolMovement CreateOpposite(Board board, ChessStatus status)
        {
            var poolMovement = new PoolMovement();
            poolMovement.GetPieceMovementForPlayer(board, status, status.PlayerTurn.Opposite(), doCheckVerification: false);
            return poolMovement;
        }

        private void GetPieceMovementForPlayer(Board board, ChessStatus status, ColorType playerTurn, bool doCheckVerification = true)
        {
            Clear();
            var pieceFieldPositions = board.GetPieceFieldPositionForPlayer(playerTurn);
            foreach (var fieldPosition in pieceFieldPositions)
            {
                var pieceMovements = PieceMovement.Movements(board, status, fieldPosition, doCheckVerification);
                movementsToList.AddRange(pieceMovements);
            }
        }

        public MovementTo GetPieceMovement(FieldPosition fromPosition, FieldPosition toPosition)
        {
            return movementsToList.FirstOrDefault(f => f.FromPosition == fromPosition && f.ToPosition == toPosition);
        }

        public MovementTo[] ShowPieceMovement(FieldPosition position)
        {
            return movementsToList.Where(f => f.FromPosition == position).ToArray();
        }

        public MovementTo[] ShowMovements()
        {
            var movementTos = new List<MovementTo>();
            foreach (var movement in movementsToList)
            {
                movementTos.AddMovement(movement.FromPosition, movement.ToPosition, movement.Movement);

            }

            return movementTos.ToArray();
        }

        private void Clear()
        {
            movementsToList = new List<MovementTo>();
        }

        public List<PoolMovementDto> ShowPoolMovement()
        {
            return movementsToList.Select(s => new PoolMovementDto
                                   {
                                       FromPosition = s.FromPosition,
                                       ToPosition = s.ToPosition,
                                       Movement = s.Movement
                                   })
                                   .ToList();
        }
    }
}
