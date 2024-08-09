using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Dtos;

namespace ChessGame.Domain.Pieces
{
    public record Piece
    {
        public PieceType Type { get; init; }
        public ColorType Color { get; init; }

        private Piece(PieceType type, ColorType color)
        {
            Type = type;
            Color = color;
        }
        public static Piece Create(PieceType type, ColorType color) => new Piece(type, color);

        public static Piece Create(PieceType? type, ColorType? color)
        {
            if(type.HasValue && color.HasValue)
            {
                return new Piece(type.Value, color.Value);
            }

            return null;
        }

        public static Piece Create(PieceBuilder pieceBuilder)
        {
            if(pieceBuilder == null)
            {
                return null;
            }

            return new Piece(pieceBuilder.Type, pieceBuilder.Color);
        }


        public bool IsColor(ColorType color) => Color == color;
        public bool IsType(PieceType type) => Type == type;

        public PieceDto ShowPiece()
        {
            return new PieceDto
            {
                Color = Color,
                Piece = Type
            };
        }
    }
}