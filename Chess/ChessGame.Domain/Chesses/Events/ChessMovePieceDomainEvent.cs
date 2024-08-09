using ChessGame.Domain.Fields;
using ChessGame.Domain.Pieces;
using ChessGame.Infrastructure.Common;

namespace ChessGame.Domain.Chesses.Events
{
    public record ChessMovePieceDomainEvent : BaseDomainEvent<Chess>
    {
        private ChessMovePieceDomainEvent()
        { }

        public ChessMovePieceDomainEvent(Chess chess, FieldPosition fromPosition, FieldPosition toPosition, PieceType? promotionPieceType) : base(chess)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
            PromotionPieceType = promotionPieceType;
        }

        public FieldPosition FromPosition { get; init; }
        public FieldPosition ToPosition { get; init; }
        public PieceType? PromotionPieceType { get; init; }
    }
}
