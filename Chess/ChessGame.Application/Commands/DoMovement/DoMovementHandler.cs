using ChessGame.Domain.Chesses;
using ChessGame.Infrastructure.Common;
using MediatR;

namespace ChessGame.Application.Commands.DoMovement
{
    public class DoMovementHandler : IRequestHandler<DoMovementCommand>
    {
        private readonly IAggregateRepository<Chess> _aggregateRepository;

        public DoMovementHandler(IAggregateRepository<Chess> aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task Handle(DoMovementCommand request, CancellationToken cancellationToken)
        {
            var aggregateEvents = await _aggregateRepository.ReadEventsAsync(request.ChessId, cancellationToken);

            var chess = Chess.Create(aggregateEvents);

            chess.MovePiece((ChessGame.Domain.Fields.FieldPosition)request.FromPosition, (ChessGame.Domain.Fields.FieldPosition)request.ToPosition, (ChessGame.Domain.Pieces.PieceType?)request.PromotionPieceType);

            await _aggregateRepository.AppendAsync(chess, cancellationToken);
        }
    }
}
