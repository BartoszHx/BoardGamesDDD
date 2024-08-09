namespace ChessGame.Domain.Dtos
{
    public record BoardDto
    {
        public FieldDto[] Fields { get; init; }
    }
}
