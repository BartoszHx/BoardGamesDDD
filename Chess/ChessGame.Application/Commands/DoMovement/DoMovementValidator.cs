using ChessGame.Application.Queries.LoadChessGame;
using FluentValidation;

namespace ChessGame.Application.Commands.DoMovement
{
    public class DoMovementValidator : AbstractValidator<DoMovementCommand>
    {
        public DoMovementValidator()
        {
            RuleFor(x => x.ChessId).NotEmpty();
        }
    }
}
