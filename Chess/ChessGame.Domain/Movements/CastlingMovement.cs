using ChessGame.Domain.Boards;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Movements
{
    internal static class CastlingMovement
    {
        public static void DoMove(Board board, MovementTo movement)
        {
            if (movement.Movement != MovementType.Castling)
            {
                return;
            }

            bool isWhiteKing = movement.FromPosition == FieldPosition.E1;
            if (isWhiteKing)
            {
                bool isCastlingOO = movement.ToPosition == FieldPosition.G1;
                if (isCastlingOO)
                {
                    board.MovePieceToField(FieldPosition.E1, FieldPosition.G1);
                    board.MovePieceToField(FieldPosition.H1, FieldPosition.F1);
                }

                bool isCastlingOOO = movement.ToPosition == FieldPosition.C1;
                if (isCastlingOOO)
                {
                    board.MovePieceToField(FieldPosition.E1, FieldPosition.C1);
                    board.MovePieceToField(FieldPosition.A1, FieldPosition.D1);
                }
            }

            bool isBlackKing = movement.FromPosition == FieldPosition.E8;
            if (isBlackKing)
            {
                bool isCastlingOO = movement.ToPosition == FieldPosition.G8;
                if (isCastlingOO)
                {
                    board.MovePieceToField(FieldPosition.E8, FieldPosition.G8);
                    board.MovePieceToField(FieldPosition.H8, FieldPosition.F8);
                }

                bool isCastlingOOO = movement.ToPosition == FieldPosition.C8;
                if (isCastlingOOO)
                {
                    board.MovePieceToField(FieldPosition.E8, FieldPosition.C8);
                    board.MovePieceToField(FieldPosition.A8, FieldPosition.D8);
                }
            }
        }

        public static MovementTo[] Movements(ColorType color, Board board, ChessStatus status)
        {
            try
            {
                IsKingDoMove(color, status);
                var rockPositions = IsRockDoMove(color, board, status);
                NoPiecesBetweenKingAndRock(board, rockPositions);
                var poolMovementOpponent = PoolMovement.CreateOpposite(board, status);
                KingIsNoCheck(color, poolMovementOpponent);
                KingCanNotPassThroughCheck(poolMovementOpponent, rockPositions);

                return PrepareMovement(rockPositions);
            }
            catch (CastlingException ex)
            {
                return new MovementTo[0];
            }
        }

        private static void IsKingDoMove(ColorType color, ChessStatus status)
        {
            bool isKingDoMove = color == ColorType.White ? status.IsWhiteKingMove : status.IsBlackKingMove;
            if (isKingDoMove)
            {
                CastlingException.CantDoCastling();
            }
        }

        private static List<FieldPosition> IsRockDoMove(ColorType color, Board board, ChessStatus status)
        {
            var rockPositions = new List<FieldPosition>();

            bool isLeftRockDoMove = color == ColorType.White ? status.IsWhiteRockA1Move : status.IsBlackRockA8Move;
            bool isRightRockDoMove = color == ColorType.White ? status.IsWhiteRockH1Move : status.IsBlackRockH8Move;

            if (!isLeftRockDoMove)
            {
                var rockPosition = color == ColorType.White ? FieldPosition.A1 : FieldPosition.A8;
                var piece = board.TakePieceInformation(rockPosition);

                var isRockPiece = piece != null && piece.IsType(PieceType.Rock) && piece.IsColor(color);
                if (isRockPiece)
                {
                    rockPositions.Add(rockPosition);
                }

            }

            if (!isRightRockDoMove)
            {
                var rockPosition = color == ColorType.White ? FieldPosition.H1 : FieldPosition.H8;
                var piece = board.TakePieceInformation(rockPosition);

                var isRockPiece = piece != null && piece.IsType(PieceType.Rock) && piece.IsColor(color);
                if (isRockPiece)
                {
                    rockPositions.Add(rockPosition);
                }
            }

            if (!rockPositions.Any())
            {
                CastlingException.CantDoCastling();
            }

            return rockPositions;
        }

        private static void NoPiecesBetweenKingAndRock(Board board, List<FieldPosition> rockPositions)
        {
            var toDeleteList = new List<FieldPosition>();

            foreach(var rockPosition in rockPositions)
            {
                var fieldsBetween = TakeFieldsBetween(rockPosition);

                foreach(var field in fieldsBetween)
                {
                    if(board.IsPieceOnFiled(field))
                    {
                        toDeleteList.Add(rockPosition);
                        break;
                    }
                }
            }

            foreach(var toDelete in toDeleteList)
            {
                rockPositions.Remove(toDelete);
            }

            if (!rockPositions.Any())
            {
                CastlingException.CantDoCastling();
            }
        }

        private static void KingIsNoCheck(ColorType color, PoolMovement poolMovementOpponent)
        {
            var kingPosition = color == ColorType.White ? FieldPosition.E1 : FieldPosition.E8;

            var kingIsCheck = poolMovementOpponent.ShowMovements().Any(a => a.ToPosition == kingPosition);
            if(kingIsCheck)
            {
                CastlingException.CantDoCastling();
            }
        }

        private static void KingCanNotPassThroughCheck(PoolMovement poolMovementOpponent, List<FieldPosition> rockPositions)
        {
            var toDeleteList = new List<FieldPosition>();

            foreach (var rockPosition in rockPositions)
            {
                var fieldsBetween = TakeFieldsBetweenCheck(rockPosition);

                foreach (var field in fieldsBetween)
                {
                    var isCheckInField = poolMovementOpponent.ShowMovements().Any(a => a.ToPosition == field);
                    if (isCheckInField)
                    {
                        toDeleteList.Add(rockPosition);
                        break;
                    }
                }
            }

            foreach (var toDelete in toDeleteList)
            {
                rockPositions.Remove(toDelete);
            }

            if (!rockPositions.Any())
            {
                CastlingException.CantDoCastling();
            }
        }

        private static MovementTo[] PrepareMovement(List<FieldPosition> rockPositions)
        {
            var movementList = new List<MovementTo>();

            foreach(var rockPosition in rockPositions)
            {
                switch(rockPosition)
                {
                    case FieldPosition.A1: movementList.AddMovement(FieldPosition.E1, FieldPosition.C1, MovementType.Castling); break;
                    case FieldPosition.H1: movementList.AddMovement(FieldPosition.E1, FieldPosition.G1, MovementType.Castling); break;
                    case FieldPosition.A8: movementList.AddMovement(FieldPosition.E8, FieldPosition.C8, MovementType.Castling); break;
                    case FieldPosition.H8: movementList.AddMovement(FieldPosition.E8, FieldPosition.G8, MovementType.Castling); break;
                }   
            }

            return movementList.ToArray();
        }

        private static FieldPosition[] TakeFieldsBetween(FieldPosition rockPosition)
        {
            if (rockPosition == FieldPosition.A1) return new FieldPosition[] { FieldPosition.B1, FieldPosition.C1, FieldPosition.D1 };
            if (rockPosition == FieldPosition.H1) return new FieldPosition[] { FieldPosition.F1, FieldPosition.G1 };
            if (rockPosition == FieldPosition.A8) return new FieldPosition[] { FieldPosition.B8, FieldPosition.C8, FieldPosition.D8 };
            if (rockPosition == FieldPosition.H8) return new FieldPosition[] { FieldPosition.F8, FieldPosition.G8 };

            return new FieldPosition[0];
        }

        private static FieldPosition[] TakeFieldsBetweenCheck(FieldPosition rockPosition)
        {
            if (rockPosition == FieldPosition.A1) return new FieldPosition[] { FieldPosition.C1, FieldPosition.D1 };
            if (rockPosition == FieldPosition.H1) return new FieldPosition[] { FieldPosition.F1, FieldPosition.G1 };
            if (rockPosition == FieldPosition.A8) return new FieldPosition[] { FieldPosition.C8, FieldPosition.D8 };
            if (rockPosition == FieldPosition.H8) return new FieldPosition[] { FieldPosition.F8, FieldPosition.G8 };

            return new FieldPosition[0];
        }
    }

    public class CastlingException : Exception
    {
        internal static void CantDoCastling() => throw new CastlingException();
    }
}
