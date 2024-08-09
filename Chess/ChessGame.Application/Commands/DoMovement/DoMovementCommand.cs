using MediatR;

namespace ChessGame.Application.Commands.DoMovement
{
    public record DoMovementCommand : IRequest
    {
        public Guid ChessId { get; init; }
        public FieldPosition FromPosition { get; init; }
        public FieldPosition ToPosition { get; init; }
        public PieceType? PromotionPieceType { get; init; } = null;
    }
}
