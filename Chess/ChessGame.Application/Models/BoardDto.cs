namespace ChessGame.Application.Models
{
    public record BoardDto
    {
        public FieldDto[] Fields { get; init; }
    }
}
