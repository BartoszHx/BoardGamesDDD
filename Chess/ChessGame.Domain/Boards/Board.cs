using ChessGame.Domain.Chesses;
using ChessGame.Domain.Dtos;
using ChessGame.Domain.Fields;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Boards
{
    public record Board
    {
        private Field[] _fields;
        private Board(Field[] fields)
        {
            _fields = fields;
        }

        public static Board Create(Field[] fields) => new Board(fields);

        public Board Copy()
        {
            var newFields = new Field[64];

            for (int i = 0; i <= 63; i++)
            {
                newFields[i] = _fields[i].Copy();
            }

            return Board.Create(newFields);
        }

        private Field GetField(FieldPosition position) => _fields.First(f => f.Position == position);

        public Piece TakePieceInformation(FieldPosition position) => GetField(position).GetPiece();

        public void MovePieceToField(FieldPosition formPosition, FieldPosition toPosition)
        {
            var fromField = GetField(formPosition);
            var toField = GetField(toPosition);

            var piece = fromField.GetPiece();
            fromField.EmptyField();
            toField.PutPieceOnField(piece);
        }

        public bool IsPieceOnFiled(FieldPosition position)
        {
            return GetField(position).IsPiece();
        }

        public FieldPosition[] GetPieceFieldPositionForPlayer(ColorType color)
        {
            return _fields.Where(w => w.IsPiece(color)).Select(s => s.ShowField().Position).ToArray();
        }

        public void ChangePiece(FieldPosition position, PieceType pieceType)
        {
            var field = GetField(position);
            var oldPiece = field.GetPiece();
            var newPiece = Piece.Create(pieceType, oldPiece.Color);
            field.PutPieceOnField(newPiece);
        }

        public FieldPosition GetKingPosition(ColorType color)
        {
            return _fields.First(w => w.IsPiece(PieceType.King, color)).Position;
        }

        public void RemovePiece(FieldPosition position) 
        {
            var field = _fields.First(f => f.Position == position);
            field.EmptyField();
        }

        public BoardDto ShowBoard()
        {
            return new BoardDto
            {
                Fields = _fields.Select(s => s.ShowField()).ToArray()
            };
        }
    }
}
