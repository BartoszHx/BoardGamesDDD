using MediatR;

namespace ChessGame.Application.Commands.CreateChessStandardGame
{
    public record CreateChessStandardGameCommand : IRequest<CreateChessStandardGameResult>
    {
    }
}
