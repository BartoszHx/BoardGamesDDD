using ChessGame.Domain.Fields;

namespace ChessGame.Domain.Dtos
{
    public record FieldDto
    {
        public FieldPosition Position { get; init; }
        public PieceDto? Piece { get; init; }
    }
}
