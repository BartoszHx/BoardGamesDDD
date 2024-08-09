namespace ChessGame.Application.Models
{
    public record PoolMovementDto
    {
        public FieldPosition FromPosition { get; init; }
        public FieldPosition ToPosition { get; init; }
        public MovementType Movement { get; init; }
    }
}
