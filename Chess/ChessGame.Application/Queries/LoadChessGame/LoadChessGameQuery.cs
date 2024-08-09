using MediatR;

namespace ChessGame.Application.Queries.LoadChessGame
{
    public class LoadChessGameQuery : IRequest<LoadChessGameResult>
    {
        public Guid ChessId { get; set; }
    }
}
