using ChessGame.Application.Models;

namespace ChessGame.Application.Queries.LoadChessGame
{
    public record LoadChessGameResult
    {
        public ChessDto Chess { get; init; }
    }
}
