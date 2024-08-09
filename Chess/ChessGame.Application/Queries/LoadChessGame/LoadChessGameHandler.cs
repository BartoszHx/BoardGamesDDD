using AutoMapper;
using ChessGame.Domain.Chesses;
using ChessGame.Infrastructure.Common;
using MediatR;

namespace ChessGame.Application.Queries.LoadChessGame
{
    public class LoadChessGameHandler : IRequestHandler<LoadChessGameQuery, LoadChessGameResult>
    {
        private readonly IAggregateRepository<Chess> _aggregateRepository;
        private readonly IMapper _mapper;

        public LoadChessGameHandler(IAggregateRepository<Chess> aggregateRepository, IMapper mapper)
        {
            _aggregateRepository = aggregateRepository;
            _mapper = mapper;
        }

        public async Task<LoadChessGameResult> Handle(LoadChessGameQuery request, CancellationToken cancellationToken)
        {
            var aggregateEvents = await _aggregateRepository.ReadEventsAsync(request.ChessId, cancellationToken);

            var chess = Chess.Create(aggregateEvents);

            var chessDto = chess.ShowChess();
            var chessResult = _mapper.Map<ChessDto>(chessDto);

            return new LoadChessGameResult { Chess = chessResult };
        }
    } 
}
