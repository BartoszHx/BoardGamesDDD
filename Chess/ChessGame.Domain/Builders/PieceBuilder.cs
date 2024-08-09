using ChessGame.Domain.Chesses;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Builders
{
    public record PieceBuilder
    {
        public PieceType Type { get; init; }
        public ColorType Color { get; init; }

        private PieceBuilder(PieceType type, ColorType color)
        {
            Type = type;
            Color = color;
        }

        public static PieceBuilder Create(PieceType type, ColorType color) => new PieceBuilder(type, color);
    }
}
