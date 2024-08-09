using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;

namespace ChessGame.Domain.Test.FourMovements
{
    [TestClass]
    public class StartMovementsTest
    {
        [TestMethod]
        public void MoveD2D4()
        {
            var chessBuilder = ChessGameBuilder.StandardGame();
            var chess = Chess.Create(chessBuilder);

            // Act
            chess.MovePiece(FieldPosition.D2, FieldPosition.D4);
            chess.MovePiece(FieldPosition.D7, FieldPosition.D5);
            chess.MovePiece(FieldPosition.G1, FieldPosition.F3);
            chess.MovePiece(FieldPosition.G8, FieldPosition.F6);

            var board = chess.ShowChess().Board;

            // Assert
            Assert.IsTrue(board.Fields.Any(f => f.Position == FieldPosition.D4 && f.Piece.Piece == Pieces.PieceType.Pawn && f.Piece.Color == ColorType.White));
            Assert.IsTrue(board.Fields.Any(f => f.Position == FieldPosition.D5 && f.Piece.Piece == Pieces.PieceType.Pawn && f.Piece.Color == ColorType.Black));
            Assert.IsTrue(board.Fields.Any(f => f.Position == FieldPosition.F3 && f.Piece.Piece == Pieces.PieceType.Knight && f.Piece.Color == ColorType.White));
            Assert.IsTrue(board.Fields.Any(f => f.Position == FieldPosition.F6 && f.Piece.Piece == Pieces.PieceType.Knight && f.Piece.Color == ColorType.Black));
        }
    }
}
