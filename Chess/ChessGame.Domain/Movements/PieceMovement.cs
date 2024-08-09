using ChessGame.Domain.Boards;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Movements
{
    internal class PieceMovement
    {
        private readonly Board board;
        private readonly ChessStatus status;

        private PieceMovement(Board board, ChessStatus status)
        {
            this.board = board;
            this.status = status;
        }

        internal static MovementTo[] Movements(Board board, ChessStatus status, FieldPosition piecePosition, bool doCheckVerification = true) 
        {
            var chessPieceMovement = new PieceMovement(board, status);
            return chessPieceMovement.PieceMoves(piecePosition, doCheckVerification);
        }

        private MovementTo[] PieceMoves(FieldPosition piecePosition, bool doCheckVerification = true)
        {
            var piece = board.TakePieceInformation(piecePosition);
            if (piece == null)
            {
                return new MovementTo[0];
            }

            var movements = new List<MovementTo>();

            switch (piece.Type)
            {
                case PieceType.Pawn: movements = PawnMovement.Movement(piece.Color, piecePosition, board, status); break;
                case PieceType.Bishop: movements = BishopMovement(piecePosition); break;
                case PieceType.Knight: movements = KnightMovement(piecePosition); break;
                case PieceType.Rock: movements = RockMovement(piecePosition); break;
                case PieceType.Queen: movements = QueenMovement(piecePosition); break;
                case PieceType.King: movements = KingMovement.Movement(piece.Color, piecePosition, board, status); break;
                default: return new MovementTo[0];
            }

            RemovedIllegalMoves(piece.Color, movements);

            if (doCheckVerification)
            {
                CheckVerification.RemoveCheckMoves(board, movements, status); 
            }

            return movements.ToArray();
        }

        private List<MovementTo> QueenMovement(FieldPosition piecePosition)
        {
            var movements = new List<MovementTo>();
            movements.AddRange(RockMovement(piecePosition));
            movements.AddRange(BishopMovement(piecePosition));
            return movements;

        }

        private List<MovementTo> RockMovement(FieldPosition piecePosition)
        {
            var movements = new List<MovementTo>();

            movements.AddRange(MoveForward(piecePosition, (p) => p.TakeUp()));
            movements.AddRange(MoveForward(piecePosition, (p) => p.TakeLeft()));
            movements.AddRange(MoveForward(piecePosition, (p) => p.TakeRight()));
            movements.AddRange(MoveForward(piecePosition, (p) => p.TakeDown()));

            return movements;
        }

        private List<MovementTo> KnightMovement(FieldPosition piecePosition)
        {
            var movements = new List<MovementTo>();

            movements.AddMovement(piecePosition, piecePosition.TakeUp()?.TakeUp()?.TakeLeft());
            movements.AddMovement(piecePosition, piecePosition.TakeUp()?.TakeUp()?.TakeRight());
            movements.AddMovement(piecePosition, piecePosition.TakeDown()?.TakeDown()?.TakeLeft());
            movements.AddMovement(piecePosition, piecePosition.TakeDown()?.TakeDown()?.TakeRight());
            movements.AddMovement(piecePosition, piecePosition.TakeLeft()?.TakeLeft()?.TakeUp());
            movements.AddMovement(piecePosition, piecePosition.TakeLeft()?.TakeLeft()?.TakeDown());
            movements.AddMovement(piecePosition, piecePosition.TakeRight()?.TakeRight()?.TakeUp());
            movements.AddMovement(piecePosition, piecePosition.TakeRight()?.TakeRight()?.TakeDown());

            return movements;
        }

        private List<MovementTo> BishopMovement(FieldPosition piecePosition)
        {
            var movements = new List<MovementTo>();

            movements.AddRange(MoveForward(piecePosition, (p) => p.TakeUpLeft()));
            movements.AddRange(MoveForward(piecePosition, (p) => p.TakeUpRight()));
            movements.AddRange(MoveForward(piecePosition, (p) => p.TakeDownLeft()));
            movements.AddRange(MoveForward(piecePosition, (p) => p.TakeDownRight()));

            return movements;
        }

        private void RemovedIllegalMoves(ColorType pieceColor, List<MovementTo> movements)
        {
            var illegalMoves = new List<MovementTo>();

            foreach (var move in movements)
            {
                var piece = board.TakePieceInformation(move.ToPosition);

                bool isEmptyField = piece == null;
                bool isEnemyField = piece != null && piece.IsColor(pieceColor.Opposite());

                bool canMove = isEmptyField || isEnemyField;

                if (!canMove)
                {
                    illegalMoves.Add(move);
                }
            }

            foreach (var illegalMove in illegalMoves)
            {
                movements.Remove(illegalMove);
            }
        }

        private List<MovementTo> MoveForward(FieldPosition piecePosition, Func<FieldPosition, FieldPosition?> takeFunction)
        {
            FieldPosition? position = piecePosition;
            var movements = new List<MovementTo>();

            do
            {
                position = takeFunction(position.Value);
                if (!position.HasValue)
                {
                    break;
                }

                var isPiece = board.IsPieceOnFiled(position.Value);
                if (isPiece)
                {
                    movements.AddMovement(piecePosition, position);
                    break;
                }

                movements.AddMovement(piecePosition, position);

            } while (position.HasValue);

            return movements;
        }
    }
}
