using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Test.Castling
{
    [TestClass]
    public class CastlingOnCustomBoardTest
    {
        [TestMethod]
        [DataRow(FieldPosition.C1)]
        [DataRow(FieldPosition.G1)]
        [DataRow(FieldPosition.C8)]
        [DataRow(FieldPosition.G8)]
        public void CanCastlingTo(FieldPosition exceptKingPosition)
        {
            // Arrange
            var kingPosition = SetStartKingPosition(exceptKingPosition);
            var chessBuilder = CreateChessBuilder(exceptKingPosition);
            var chess = Chess.Create(chessBuilder);

            // Act
            var movements = chess.PieceMovements(kingPosition);

            // Assert
            Assert.IsTrue(movements.Any(a => a == exceptKingPosition));
        }

        [TestMethod]
        [DataRow(FieldPosition.C1, FieldPosition.D1)]
        [DataRow(FieldPosition.G1, FieldPosition.F1)]
        [DataRow(FieldPosition.C8, FieldPosition.D8)]
        [DataRow(FieldPosition.G8, FieldPosition.F8)]
        public void DoCastlingTo(FieldPosition exceptKingPosition, FieldPosition exceptRockPosition)
        {
            // Arrange
            var kingPosition = SetStartKingPosition(exceptKingPosition);
            var chessBuilder = CreateChessBuilder(exceptKingPosition);
            var chess = Chess.Create(chessBuilder);

            // Act
            chess.MovePiece(kingPosition, exceptKingPosition);

            // Assert
            var chessBoard = chess.ShowChess().Board;
            Assert.IsTrue(chessBoard.Fields.Any(a => a.Position == exceptKingPosition && a.Piece?.Piece == PieceType.King));
            Assert.IsTrue(chessBoard.Fields.Any(a => a.Position == exceptRockPosition && a.Piece?.Piece == PieceType.Rock));
        }

        [TestMethod]
        [DataRow(FieldPosition.C1, FieldPosition.D1)]
        [DataRow(FieldPosition.C1, FieldPosition.C1)]
        [DataRow(FieldPosition.C1, FieldPosition.B1)]
        [DataRow(FieldPosition.C1, FieldPosition.D1, FieldPosition.C1)]
        [DataRow(FieldPosition.C1, FieldPosition.D1, FieldPosition.B1)]
        [DataRow(FieldPosition.C1, FieldPosition.D1, FieldPosition.C1, FieldPosition.B1)]
        [DataRow(FieldPosition.G1, FieldPosition.F1)]
        [DataRow(FieldPosition.G1, FieldPosition.G1)]
        [DataRow(FieldPosition.G1, FieldPosition.F1, FieldPosition.G1)]
        [DataRow(FieldPosition.C8, FieldPosition.D8)]
        [DataRow(FieldPosition.C8, FieldPosition.C8)]
        [DataRow(FieldPosition.C8, FieldPosition.B8)]
        [DataRow(FieldPosition.C8, FieldPosition.D8, FieldPosition.C8)]
        [DataRow(FieldPosition.C8, FieldPosition.D8, FieldPosition.B8)]
        [DataRow(FieldPosition.C8, FieldPosition.D8, FieldPosition.C8, FieldPosition.B8)]
        [DataRow(FieldPosition.G8, FieldPosition.F8)]
        [DataRow(FieldPosition.G8, FieldPosition.G8)]
        [DataRow(FieldPosition.G8, FieldPosition.F8, FieldPosition.G8)]
        public void CanNotCastlingBetweenPiecesTo(FieldPosition exceptKingPosition, params FieldPosition[] piecesPosition)
        {
            // Arrange
            var color = GetPlayerColor(exceptKingPosition);
            var kingPosition = SetStartKingPosition(exceptKingPosition);
            var chessBuilder = CreateChessBuilder(exceptKingPosition);
            foreach(var position in piecesPosition) 
            {
                chessBuilder.AddPiece(position, PieceType.Knight, color);
            }

            var chess = Chess.Create(chessBuilder);

            // Act
            var movements = chess.PieceMovements(kingPosition);

            // Assert
            Assert.IsFalse(movements.Any(a => a == exceptKingPosition));
        }

        private ChessGameBuilder CreateChessBuilder(FieldPosition exceptKingPosition)
        {
            if(exceptKingPosition == FieldPosition.C1)
            {
                return ChessGameBuilder
                    .Start()
                    .SetPlayerTurn(ColorType.White)
                    .AddPiece(FieldPosition.E1, PieceType.King, ColorType.White)
                    .AddPiece(FieldPosition.E8, PieceType.King, ColorType.Black)
                    .AddPiece(FieldPosition.A1, PieceType.Rock, ColorType.White);
            }

            if (exceptKingPosition == FieldPosition.G1)
            {
                return ChessGameBuilder
                    .Start()
                    .SetPlayerTurn(ColorType.White)
                    .AddPiece(FieldPosition.E1, PieceType.King, ColorType.White)
                    .AddPiece(FieldPosition.E8, PieceType.King, ColorType.Black)
                    .AddPiece(FieldPosition.H1, PieceType.Rock, ColorType.White);
            }

            if(exceptKingPosition == FieldPosition.C8)
            {
                return ChessGameBuilder
                    .Start()
                    .SetPlayerTurn(ColorType.Black)
                    .AddPiece(FieldPosition.E8, PieceType.King, ColorType.Black)
                    .AddPiece(FieldPosition.E1, PieceType.King, ColorType.White)
                    .AddPiece(FieldPosition.A8, PieceType.Rock, ColorType.Black);
            }

            if (exceptKingPosition == FieldPosition.G8)
            {
                return ChessGameBuilder
                    .Start()
                    .SetPlayerTurn(ColorType.Black)
                    .AddPiece(FieldPosition.E8, PieceType.King, ColorType.Black)
                    .AddPiece(FieldPosition.E1, PieceType.King, ColorType.White)
                    .AddPiece(FieldPosition.H8, PieceType.Rock, ColorType.Black);
            }

            throw new Exception("Wrong exceptKingPosition");
        }

        private FieldPosition SetStartKingPosition(FieldPosition exceptKingPosition)
        {
            if(exceptKingPosition == FieldPosition.C1 || exceptKingPosition == FieldPosition.G1)
            {
                return FieldPosition.E1;
            }

            if (exceptKingPosition == FieldPosition.C8 || exceptKingPosition == FieldPosition.G8)
            {
                return FieldPosition.E8;
            }

            throw new Exception("Wrong exceptKingPosition");
        }

        private ColorType GetPlayerColor(FieldPosition exceptKingPosition)
        {
            if (exceptKingPosition == FieldPosition.C1 || exceptKingPosition == FieldPosition.G1)
            {
                return ColorType.White;
            }

            if (exceptKingPosition == FieldPosition.C8 || exceptKingPosition == FieldPosition.G8)
            {
                return ColorType.Black;
            }

            throw new Exception("Wrong exceptKingPosition");
        }
    }
}
