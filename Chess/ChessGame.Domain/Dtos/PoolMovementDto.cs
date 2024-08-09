using ChessGame.Domain.Fields;
using ChessGame.Domain.Movements;

namespace ChessGame.Domain.Dtos
{
    public record PoolMovementDto
    {
        public FieldPosition FromPosition { get; init; }
        public FieldPosition ToPosition { get; init; }
        public MovementType Movement { get; init; }
    }
}
