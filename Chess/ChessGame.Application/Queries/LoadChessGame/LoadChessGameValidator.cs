using FluentValidation;

namespace ChessGame.Application.Queries.LoadChessGame
{
    public class LoadChessGameValidator : AbstractValidator<LoadChessGameQuery>
    {
        public LoadChessGameValidator()
        {
            RuleFor(x => x.ChessId).NotEmpty();
        }
    }
}
