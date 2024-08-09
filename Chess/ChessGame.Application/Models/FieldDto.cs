namespace ChessGame.Application.Models
{
    public record FieldDto
    {
        public FieldPosition Position { get; init; }
        public PieceDto? Piece { get; init; }
    }
}
