using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses;
using ChessGame.Infrastructure.Common;
using MediatR;

namespace ChessGame.Application.Commands.CreateChessStandardGame
{
    public class CreateChessStandardGameHandler : IRequestHandler<CreateChessStandardGameCommand, CreateChessStandardGameResult>
    {
        private IAggregateRepository<Chess> _aggregateRepository;

        public CreateChessStandardGameHandler(IAggregateRepository<Chess> aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public Task<CreateChessStandardGameResult> Handle(CreateChessStandardGameCommand request, CancellationToken cancellationToken)
        {
            var chess = Chess.Create(ChessGameBuilder.StandardGame());

            _aggregateRepository.AppendAsync(chess, cancellationToken);

            return Task.FromResult(new CreateChessStandardGameResult { ChessId = chess.Id });
        }
    }
}
