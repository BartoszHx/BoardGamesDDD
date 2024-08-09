namespace ChessGame.Application.Models
{
    public record ChessDto
    {
        public Guid Id { get; init; }
        public BoardDto Board { get; init; }
        public List<PoolMovementDto> PoolMovements { get; init; }
        public ColorType PlayerTurn { get; init; }
    }
}
