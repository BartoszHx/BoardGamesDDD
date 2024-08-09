using ChessGame.Domain.Builders;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Pieces;
using ChessGame.Infrastructure.Common;

namespace ChessGame.Domain.Chesses.Events
{
    public record ChessCreateGameDomainEvent : BaseDomainEvent<Chess>
    {
        private ChessCreateGameDomainEvent()
        { }

        public ChessCreateGameDomainEvent(Chess chess) : base(chess)
        {
            var chessBoard = chess.ShowChess().Board;
            var chessStatus = chess.GetStatus();

            var fieldsEvent = chessBoard.Fields
                .Select(s => new FieldEvent
                {
                    Position = s.Position,
                    Piece = s.Piece != null ? new PieceEvent { Color = s.Piece.Color, Type = s.Piece.Piece} : null
                });

            var statusEvent = new ChessStatusEvent
            {
                PawnDoDoubleMove = chessStatus.PawnDoDoubleMove,
                PlayerTurn = chessStatus.PlayerTurn,
                IsWhiteKingMove = chessStatus.IsWhiteKingMove,
                IsWhiteRockA1Move = chessStatus.IsWhiteRockA1Move,
                IsWhiteRockH1Move = chessStatus.IsWhiteRockH1Move,
                IsWhiteDoCastling = chessStatus.IsWhiteDoCastling,
                IsBlackKingMove = chessStatus.IsBlackKingMove,
                IsBlackRockA8Move = chessStatus.IsBlackRockA8Move,
                IsBlackRockH8Move = chessStatus.IsBlackRockH8Move,
                IsBlackDoCastling = chessStatus.IsBlackDoCastling,
            };

            Board = new BoardEvent{ Fields = fieldsEvent.ToList() };
            Status = statusEvent;
        }

        public BoardEvent Board { get; init; }
        public ChessStatusEvent Status { get; init; }

    }

    public record BoardEvent
    {
        public List<FieldEvent> Fields { get; init; } = new List<FieldEvent>();
    }

    public record FieldEvent
    {
        public FieldPosition Position { get; init; }
        public PieceEvent Piece { get; set; }
    }

    public record PieceEvent
    {
        public PieceType Type { get; init; }
        public ColorType Color { get; init; }
    }

    public record ChessStatusEvent
    {
        public FieldPosition? PawnDoDoubleMove { get; init; }
        public ColorType PlayerTurn { get; init; }
        public bool IsWhiteKingMove { get; init; }
        public bool IsWhiteRockA1Move { get; init; }
        public bool IsWhiteRockH1Move { get; init; }
        public bool IsWhiteDoCastling { get; init; }
        public bool IsBlackKingMove { get; init; }
        public bool IsBlackRockA8Move { get; init; }
        public bool IsBlackRockH8Move { get; init; }
        public bool IsBlackDoCastling { get; init; }
    }
}
