using ChessGame.Domain.Boards;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;

namespace ChessGame.Domain.Movements
{
    internal class PawnMovement
    {
        private static FieldPosition[] _whiteStartPosition = new FieldPosition[] {FieldPosition.A2, FieldPosition.B2, FieldPosition.C2, FieldPosition.D2, FieldPosition.E2, FieldPosition.F2, FieldPosition.G2, FieldPosition.H2 };
        private static FieldPosition[] _blackStartPosition = new FieldPosition[] {FieldPosition.A7, FieldPosition.B7, FieldPosition.C7, FieldPosition.D7, FieldPosition.E7, FieldPosition.F7, FieldPosition.G7, FieldPosition.H7 };

        private readonly ColorType color;
        private readonly FieldPosition piecePosition;
        private readonly Board board;
        private readonly ChessStatus status;
        private readonly List<MovementTo> movementList;

        private PawnMovement(ColorType color, FieldPosition piecePosition, Board board, ChessStatus status)
        {
            this.color = color;
            this.piecePosition = piecePosition;
            this.board = board;
            this.status = status;
            movementList = new List<MovementTo>();
        }

        public static List<MovementTo> Movement(ColorType color, FieldPosition piecePosition, Board board, ChessStatus status)
        {
            var pawnMovement = new PawnMovement(color, piecePosition, board, status);
            return pawnMovement.Movement();
        }

        private List<MovementTo> Movement()
        {
            OneForward();
            TwoForward();
            Capture();
            EnPassant();
            Promotion();

            return this.movementList;
        }

        private FieldPosition? MoveForward(FieldPosition piecePositionMove, ColorType colorMove)
        {
            return colorMove == ColorType.White ? piecePositionMove.TakeUp() : piecePositionMove.TakeDown();
        }

        private void OneForward()
        {
            var oneForward = MoveForward(piecePosition, color);
            var canMoveForward = oneForward.HasValue && !board.IsPieceOnFiled(oneForward.Value);
            if (canMoveForward)
            {
                movementList.AddMovement(piecePosition, oneForward.Value);
            }
        }

        private void TwoForward()
        {
            var startPosition = color == ColorType.White ? _whiteStartPosition : _blackStartPosition;
            var isInStartPosition = startPosition.Any(a => a == piecePosition);
            if(!isInStartPosition)
            {
                return;
            }

            var firstMove = MoveForward(piecePosition, color);
            var canDoFirstMove = firstMove.HasValue && !board.IsPieceOnFiled(firstMove.Value);
            if (!canDoFirstMove)
            {
                return;
            }

            var secondMove = MoveForward(firstMove.Value, color);
            var canDoSecendMove = secondMove.HasValue && !board.IsPieceOnFiled(secondMove.Value);
            if (canDoSecendMove)
            {
                movementList.AddMovement(piecePosition, secondMove.Value);
            }
        }

        private void Capture()
        {
            var moveLeft = color == ColorType.White ? piecePosition.TakeUpLeft() : piecePosition.TakeDownLeft();
            var moveRight = color == ColorType.White ? piecePosition.TakeUpRight() : piecePosition.TakeDownRight();

            MoveCapture(moveLeft);
            MoveCapture(moveRight);
        }

        private void MoveCapture(FieldPosition? position)
        {
            if (position.HasValue)
            {
                var fieldOnLeft = board.TakePieceInformation(position.Value);
                if (fieldOnLeft != null && !fieldOnLeft.IsColor(color))
                {
                    movementList.AddMovement(piecePosition, position.Value);
                }
            }
        }

        private void EnPassant()
        {
            var leftPosition = piecePosition.TakeLeft();
            var rightPosition = piecePosition.TakeRight();

            MoveEnPassant(leftPosition);
            MoveEnPassant(rightPosition);
        }

        private void MoveEnPassant(FieldPosition? position)
        {
            if (position.HasValue && board.IsPieceOnFiled(position.Value))
            {
                if(status.IsPawnDoDoubleMove(position.Value))
                {
                    var movePosition = MoveForward(position.Value, color);
                    movementList.AddMovement(piecePosition, movePosition.Value);
                }
            }
        }

        private void Promotion()
        {
            var promotionMoves = movementList.Where(w => w.ToPosition.IsEighthRankField()).ToList();
            foreach (var promotion in promotionMoves)
            {
                movementList.Remove(promotion);
                movementList.AddMovement(promotion.FromPosition, promotion.ToPosition, MovementType.Promotion);
            }
        }
    }
}
