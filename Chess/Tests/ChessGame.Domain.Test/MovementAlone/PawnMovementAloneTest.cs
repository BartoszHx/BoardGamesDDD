using ChessGame.Domain.Fields;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Pieces;
using ChessGame.Domain.Builders;

namespace ChessGame.Domain.Test.MovementAlone
{
    [TestClass]
    public class PawnMovementAloneTest
    {
        [TestMethod]
        [DataRow(FieldPosition.A2, ColorType.White)]
        [DataRow(FieldPosition.B2, ColorType.White)]
        [DataRow(FieldPosition.C2, ColorType.White)]
        [DataRow(FieldPosition.D2, ColorType.White)]
        [DataRow(FieldPosition.E2, ColorType.White)]
        [DataRow(FieldPosition.F2, ColorType.White)]
        [DataRow(FieldPosition.G2, ColorType.White)]
        [DataRow(FieldPosition.H2, ColorType.White)]
        [DataRow(FieldPosition.A7, ColorType.Black)]
        [DataRow(FieldPosition.B7, ColorType.Black)]
        [DataRow(FieldPosition.C7, ColorType.Black)]
        [DataRow(FieldPosition.D7, ColorType.Black)]
        [DataRow(FieldPosition.E7, ColorType.Black)]
        [DataRow(FieldPosition.F7, ColorType.Black)]
        [DataRow(FieldPosition.G7, ColorType.Black)]
        [DataRow(FieldPosition.H7, ColorType.Black)]
        public void StartPosition(FieldPosition startPosition, ColorType color)
        {
            // Arrange
            var chessBuilder = ChessGameBuilder
                .Start()
                .SetPlayerTurn(color)
                .AddPiece(FieldPosition.E1, PieceType.King, ColorType.White)
                .AddPiece(FieldPosition.E8, PieceType.King, ColorType.Black)
                .AddPiece(startPosition, PieceType.Pawn, color);

            var chess = Chess.Create(chessBuilder);

            // Act
            var movements = chess.PieceMovements(startPosition);

            // Assert
            Assert.AreEqual(2, movements.Length);
        }
    }
}
