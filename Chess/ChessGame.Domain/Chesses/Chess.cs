using ChessGame.Domain.Boards;
using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses.Events;
using ChessGame.Domain.Dtos;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Movements;
using ChessGame.Domain.Pieces;
using ChessGame.Domain.Rules.MovePiece;
using ChessGame.Domain.Rules.Promotion;
using ChessGame.Infrastructure.Common;

namespace ChessGame.Domain.Chesses
{
    public class Chess : EntityAggregateRoot
    {
        private Board _board;
        private PoolMovement _poolMovement;
        private ChessStatus _status;

        public ColorType PlayerTurn => _status.PlayerTurn;

        private Chess() { }

        private Chess(Guid id, Board board, ChessStatus status)
        {
            Id = id;
            _board = board;
            _status = status;
            _poolMovement = PoolMovement.Create(_board, _status);
            AddEvent(new ChessCreateGameDomainEvent(this));
        }

        public static Chess Create(ChessGameBuilder gameBuilder) => new Chess(Guid.NewGuid(), Board.Create(Field.CreateArray(gameBuilder.Fields)), ChessStatus.Create(gameBuilder.StatusBuilder));

        public static Chess Create(IEnumerable<IDomainEvent> events)
        {
            var chess = new Chess();
            chess.Rebuild(events);
            return chess;
        }

        public void MovePiece(FieldPosition fromPosition, FieldPosition toPosition, PieceType? promotionPieceType = null)
        {
            var piece = _board.TakePieceInformation(fromPosition);
            var movement = _poolMovement.GetPieceMovement(fromPosition, toPosition);

            bool isNormalMove = !promotionPieceType.HasValue;
            if(isNormalMove)
            {
                CheckRulesForMovePiece(piece, movement);
            }
            else
            {
                CheckRulesForMovePiecePromotion(piece, movement, promotionPieceType.Value);
            }

            DoMove.MovePieceOnBoard(_board, _status, movement, promotionPieceType);

            _status.UpdateMoveStatus(_board, movement);

            NextPlayer();
            AddEvent(new ChessMovePieceDomainEvent(this, fromPosition, toPosition, promotionPieceType));
        }

        public FieldPosition[] PieceMovements(FieldPosition position)
        {
            return _poolMovement.ShowPieceMovement(position).Select(s => s.ToPosition).ToArray();
        }

        public ChessDto ShowChess()
        {
            return new ChessDto
            {
                Id = Id,
                Board = _board.ShowBoard(),
                PoolMovements = _poolMovement.ShowPoolMovement(),
                PlayerTurn = _status.PlayerTurn
            };
        }

        public ChessStatus GetStatus() => this._status;

        private void CheckRulesForMovePiece(Piece? piece, MovementTo movement)
        {
            CheckRule(new PieceIsInFieldRule(piece));
            CheckRule(new PlayerTurnRule(_status.PlayerTurn, piece));
            CheckRule(new PieceCanMoveRule(movement));
            CheckRule(new NotPromotionMoveRule(movement));
        }

        private void CheckRulesForMovePiecePromotion(Piece? piece, MovementTo movement, PieceType promotionPieceType)
        {
            CheckRule(new PieceIsInFieldRule(piece));
            CheckRule(new PlayerTurnRule(_status.PlayerTurn, piece));
            CheckRule(new PieceCanMoveRule(movement));
            CheckRule(new PromotionMoveRule(movement));
            CheckRule(new ChoseCorrectPieceTypeToPromotionRule(promotionPieceType));
        }

        private void NextPlayer()
        {
            _status.ChangePlayerTurn();
            _poolMovement = PoolMovement.Update(_board, _status);
        }

        protected override void Apply(IDomainEvent @event)
        {
            switch(@event)
            {
                case ChessCreateGameDomainEvent chessCreateStandard: OnChessCreateStandardGameDomainEvent(chessCreateStandard); break;
                case ChessMovePieceDomainEvent chessMovePiece: OnChessMovePieceDomainEvent(chessMovePiece); break;
            }
        }

        private void OnChessCreateStandardGameDomainEvent(ChessCreateGameDomainEvent @event)
        {
            Id = @event.AggregateId;
            Version = @event.AggregateVersion;

            var fields = @event.Board.Fields.Select(s => Field.Create(s.Position, Piece.Create(s.Piece?.Type, s.Piece?.Color)));

            _board = Board.Create(fields.ToArray());
            _status = ChessStatus.Create(@event.Status);
            _poolMovement = PoolMovement.Create(_board, _status);
        }

        private void OnChessMovePieceDomainEvent(ChessMovePieceDomainEvent @event)
        {
            MovePiece(@event.FromPosition, @event.ToPosition, @event.PromotionPieceType);

            Id = @event.AggregateId;
            Version = @event.AggregateVersion;
        }
    }
}
