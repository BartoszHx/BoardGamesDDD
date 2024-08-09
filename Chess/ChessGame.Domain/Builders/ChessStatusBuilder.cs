using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;

namespace ChessGame.Domain.Builders
{
    public record ChessStatusBuilder
    {
        public FieldPosition? PawnDoDoubleMove { get; set; }
        public ColorType? PlayerTurn { get; set; }
        public bool IsWhiteKingMove { get; set; }
        public bool IsWhiteRockA1Move { get; set; }
        public bool IsWhiteRockH1Move { get; set; }
        public bool IsWhiteDoCastling { get; set; }
        public bool IsBlackKingMove { get; set; }
        public bool IsBlackRockA8Move { get; set; }
        public bool IsBlackRockH8Move { get; set; }
        public bool IsBlackDoCastling { get; set; }
    }
}
