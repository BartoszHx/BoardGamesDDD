namespace ChessGame.Application.Options
{
    public record ApiCorsOptions
    {
        public string[] Origins { get; init; }
    }
}
