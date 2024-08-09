using ChessGame.Domain.Boards;
using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses.Events;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Movements;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Chesses
{
    public class ChessStatus
    {
        public FieldPosition? PawnDoDoubleMove { get; private set; }

        public ColorType PlayerTurn { get; private set; }
        public bool IsWhiteKingMove { get; private set; }
        public bool IsWhiteRockA1Move { get; private set; }
        public bool IsWhiteRockH1Move { get; private set; }
        public bool IsWhiteDoCastling { get; private set; }
        public bool IsBlackKingMove { get; private set; }
        public bool IsBlackRockA8Move { get; private set; }
        public bool IsBlackRockH8Move { get; private set; }
        public bool IsBlackDoCastling { get; private set; }

        private ChessStatus(ColorType playerTurn)
        {
            PlayerTurn = playerTurn;
        }

        private ChessStatus(ChessStatusBuilder builder)
        {
            PlayerTurn = builder.PlayerTurn.Value;
            PawnDoDoubleMove = builder.PawnDoDoubleMove;
            IsWhiteKingMove = builder.IsWhiteKingMove;
            IsWhiteRockA1Move = builder.IsWhiteRockA1Move;
            IsWhiteRockH1Move = builder.IsWhiteRockH1Move;
            IsWhiteDoCastling = builder.IsWhiteDoCastling;
            IsBlackKingMove = builder.IsBlackKingMove;
            IsBlackRockA8Move = builder.IsBlackRockA8Move;
            IsBlackRockH8Move = builder.IsBlackRockH8Move;
            IsBlackDoCastling = builder.IsBlackDoCastling;
        }

        private ChessStatus(ChessStatusEvent statusEvent)
        {
            PlayerTurn = statusEvent.PlayerTurn;
            PawnDoDoubleMove = statusEvent.PawnDoDoubleMove;
            IsWhiteKingMove = statusEvent.IsWhiteKingMove;
            IsWhiteRockA1Move = statusEvent.IsWhiteRockA1Move;
            IsWhiteRockH1Move = statusEvent.IsWhiteRockH1Move;
            IsWhiteDoCastling = statusEvent.IsWhiteDoCastling;
            IsBlackKingMove = statusEvent.IsBlackKingMove;
            IsBlackRockA8Move = statusEvent.IsBlackRockA8Move;
            IsBlackRockH8Move = statusEvent.IsBlackRockH8Move;
            IsBlackDoCastling = statusEvent.IsBlackDoCastling;
        }

        internal static ChessStatus Create(ColorType playerTurn = ColorType.White) => new ChessStatus(playerTurn);
        internal static ChessStatus Create(ChessStatusBuilder builder) => new ChessStatus(builder);
        internal static ChessStatus Create(ChessStatusEvent statusEvent) => new ChessStatus(statusEvent);

        internal ChessStatus Copy()
        {
            var chessStatus = Create(this.PlayerTurn);
            chessStatus.PawnDoDoubleMove = this.PawnDoDoubleMove;
            chessStatus.IsWhiteKingMove = this.IsWhiteKingMove;
            chessStatus.IsWhiteRockA1Move = this.IsWhiteRockA1Move;
            chessStatus.IsWhiteRockH1Move = this.IsWhiteRockH1Move;
            chessStatus.IsWhiteDoCastling = this.IsWhiteDoCastling;
            chessStatus.IsBlackKingMove = this.IsBlackKingMove;
            chessStatus.IsBlackRockA8Move = this.IsBlackRockA8Move;
            chessStatus.IsBlackRockH8Move = this.IsBlackRockH8Move;
            chessStatus.IsBlackDoCastling = this.IsBlackDoCastling;

            return chessStatus;
        }

        public void UpdateMoveStatus(Board board, MovementTo movementTo)
        {
            var piece = board.TakePieceInformation(movementTo.ToPosition);
            WhitePlayerMoveStatus(movementTo, piece);
            BlackPlayerMoveStatus(movementTo, piece);
            DoPawnDoubleMove(movementTo, piece);
        }

        public void ChangePlayerTurn() => PlayerTurn = PlayerTurn.Opposite();

        public bool IsPawnDoDoubleMove(FieldPosition position) => PawnDoDoubleMove.HasValue && PawnDoDoubleMove.Value == position;

        private void WhitePlayerMoveStatus(MovementTo movementTo, Piece piece)
        {
            if (PlayerTurn != ColorType.Black)
            {
                return;
            }

            if (piece.IsType(PieceType.King))
            {
                IsWhiteKingMove = true;
            }

            if (piece.IsType(PieceType.Rock) && movementTo.FromPosition == FieldPosition.A1)
            {
                IsWhiteRockA1Move = true;
            }

            if (piece.IsType(PieceType.Rock) && movementTo.FromPosition == FieldPosition.H1)
            {
                IsWhiteRockH1Move = true;
            }

            if (movementTo.Movement == MovementType.Castling)
            {
                IsWhiteDoCastling = true;
            }
        }

        private void BlackPlayerMoveStatus(MovementTo movementTo, Piece piece)
        {
            if (PlayerTurn != ColorType.White)
            {
                return;
            }

            if (piece.IsType(PieceType.King))
            {
                IsBlackKingMove = true;
            }

            if (piece.IsType(PieceType.Rock) && movementTo.FromPosition == FieldPosition.A8)
            {
                IsBlackRockA8Move = true;
            }

            if (piece.IsType(PieceType.Rock) && movementTo.FromPosition == FieldPosition.H8)
            {
                IsBlackRockH8Move = true;
            }

            if (movementTo.Movement == MovementType.Castling)
            {
                IsBlackDoCastling = true;
            }
        }

        private void DoPawnDoubleMove(MovementTo movementTo, Piece piece)
        {
            if (!piece.IsType(PieceType.Pawn))
            {
                return;
            }

            var isStartPosition = PlayerTurn == ColorType.White ? movementTo.FromPosition.IsBetween(FieldPosition.A2, FieldPosition.H2) : movementTo.FromPosition.IsBetween(FieldPosition.A7, FieldPosition.H7);
            var isDoublePosition = PlayerTurn == ColorType.White ? movementTo.ToPosition.IsBetween(FieldPosition.A4, FieldPosition.H4) : movementTo.ToPosition.IsBetween(FieldPosition.A5, FieldPosition.H5);

            PawnDoDoubleMove = isStartPosition && isDoublePosition ? movementTo.ToPosition : null;
        }
    }
}
