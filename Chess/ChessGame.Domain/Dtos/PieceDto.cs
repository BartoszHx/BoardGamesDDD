using ChessGame.Domain.Chesses;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Dtos
{
    public record PieceDto
    {
        public PieceType Piece { get; init; }
        public ColorType Color { get; init; }
    }
}
