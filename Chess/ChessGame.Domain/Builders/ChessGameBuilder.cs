using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Pieces;
using ChessGame.Domain.Rules.ChessCustomBoard;

namespace ChessGame.Domain.Builders
{
    public class ChessGameBuilder : Entity
    {
        public ChessStatusBuilder StatusBuilder { get; init; }
        public FieldBuilder[] Fields { get; init; }

        private ChessGameBuilder()
        {
            Fields = FieldBuilder.CreateStandardFields();
            StatusBuilder = new ChessStatusBuilder();
        }

        public static ChessGameBuilder Start() => new ChessGameBuilder();

        public ChessGameBuilder AddPiece(FieldPosition position, PieceType pieceType, ColorType pieceColor)
        {
            var piece = PieceBuilder.Create(pieceType, pieceColor);
            var field = Fields.First(f => f.Position == position);
            field.Piece = piece;
            return this;
        }

        public ChessGameBuilder SetPlayerTurn(ColorType color)
        {
            StatusBuilder.PlayerTurn = color;
            return this;
        }

        public ChessGameBuilder DoMove(FieldPosition position)
        {
            if (position == FieldPosition.A1) StatusBuilder.IsWhiteRockA1Move = true;
            if (position == FieldPosition.H1) StatusBuilder.IsWhiteRockH1Move = true;
            if (position == FieldPosition.F1) StatusBuilder.IsWhiteKingMove = true;
            if (position == FieldPosition.A8) StatusBuilder.IsBlackRockA8Move = true;
            if (position == FieldPosition.H8) StatusBuilder.IsBlackRockH8Move = true;
            if (position == FieldPosition.F8) StatusBuilder.IsBlackKingMove = true;

            return this;
        }

        internal void CheckRules()
        {
            CheckRule(new PlayerTurnIsSet(StatusBuilder.PlayerTurn));
            CheckRule(new IsKingOnBoard(kingColor: ColorType.White, Fields));
            CheckRule(new IsKingOnBoard(kingColor: ColorType.Black, Fields));
            CheckRule(new OneKingOnBoard(kingColor: ColorType.White, Fields));
            CheckRule(new OneKingOnBoard(kingColor: ColorType.Black, Fields));
        }

        public static ChessGameBuilder StandardGame()
        {
            return 
                Start()
                .SetPlayerTurn(ColorType.White)
                .AddPiece(FieldPosition.A1, PieceType.Rock, ColorType.White)
                .AddPiece(FieldPosition.B1, PieceType.Knight, ColorType.White)
                .AddPiece(FieldPosition.C1, PieceType.Bishop, ColorType.White)
                .AddPiece(FieldPosition.D1, PieceType.Queen, ColorType.White)
                .AddPiece(FieldPosition.E1, PieceType.King, ColorType.White)
                .AddPiece(FieldPosition.F1, PieceType.Bishop, ColorType.White)
                .AddPiece(FieldPosition.G1, PieceType.Knight, ColorType.White)
                .AddPiece(FieldPosition.H1, PieceType.Rock, ColorType.White)
                .AddPiece(FieldPosition.A2, PieceType.Pawn, ColorType.White)
                .AddPiece(FieldPosition.B2, PieceType.Pawn, ColorType.White)
                .AddPiece(FieldPosition.C2, PieceType.Pawn, ColorType.White)
                .AddPiece(FieldPosition.D2, PieceType.Pawn, ColorType.White)
                .AddPiece(FieldPosition.E2, PieceType.Pawn, ColorType.White)
                .AddPiece(FieldPosition.F2, PieceType.Pawn, ColorType.White)
                .AddPiece(FieldPosition.G2, PieceType.Pawn, ColorType.White)
                .AddPiece(FieldPosition.H2, PieceType.Pawn, ColorType.White)
                .AddPiece(FieldPosition.A8, PieceType.Rock, ColorType.Black)
                .AddPiece(FieldPosition.B8, PieceType.Knight, ColorType.Black)
                .AddPiece(FieldPosition.C8, PieceType.Bishop, ColorType.Black)
                .AddPiece(FieldPosition.D8, PieceType.Queen, ColorType.Black)
                .AddPiece(FieldPosition.E8, PieceType.King, ColorType.Black)
                .AddPiece(FieldPosition.F8, PieceType.Bishop, ColorType.Black)
                .AddPiece(FieldPosition.G8, PieceType.Knight, ColorType.Black)
                .AddPiece(FieldPosition.H8, PieceType.Rock, ColorType.Black)
                .AddPiece(FieldPosition.A7, PieceType.Pawn, ColorType.Black)
                .AddPiece(FieldPosition.B7, PieceType.Pawn, ColorType.Black)
                .AddPiece(FieldPosition.C7, PieceType.Pawn, ColorType.Black)
                .AddPiece(FieldPosition.D7, PieceType.Pawn, ColorType.Black)
                .AddPiece(FieldPosition.E7, PieceType.Pawn, ColorType.Black)
                .AddPiece(FieldPosition.F7, PieceType.Pawn, ColorType.Black)
                .AddPiece(FieldPosition.G7, PieceType.Pawn, ColorType.Black)
                .AddPiece(FieldPosition.H7, PieceType.Pawn, ColorType.Black);
        }
    }
}
