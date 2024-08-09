namespace ChessGame.Application.Models
{
    public record PieceDto
    {
        public PieceType Piece { get; init; }
        public ColorType Color { get; init; }
    }
}
