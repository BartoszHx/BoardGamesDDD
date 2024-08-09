using ChessGame.Domain.Chesses;

namespace ChessGame.Domain.Rules.ChessCustomBoard
{
    internal class PlayerTurnIsSet : IBusinessRule
    {
        private readonly ColorType? playerTurn;

        public string Message => $"Player turn is not set";

        public PlayerTurnIsSet(ColorType? playerTurn)
        {
            this.playerTurn = playerTurn;
        }

        public bool IsBroken() => !playerTurn.HasValue;
    }
}
