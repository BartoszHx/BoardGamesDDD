using ChessGame.Domain.Builders;
using ChessGame.Domain.Chesses;
using ChessGame.Domain.Dtos;
using ChessGame.Domain.Pieces;

namespace ChessGame.Domain.Fields
{
    public record Field
    {
        public FieldPosition Position { get; init; }

        private Piece _piece;

        private Field(FieldPosition position)
        {
            Position = position;
        }

        private Field(FieldPosition position, Piece piece)
        {
            Position = position;
            _piece = piece;
        }

        private Field(FieldBuilder fieldBuilder)
        {
            Position = fieldBuilder.Position;
            _piece = Piece.Create(fieldBuilder.Piece);
        }

        public static Field Create(FieldBuilder fieldBuilder) => new Field(fieldBuilder);
        //To delete
        public static Field Create(FieldPosition position) => new Field(position);
        public static Field Create(FieldPosition position, Piece piece) => new Field(position, piece);

        public static Field[] CreateArray(FieldBuilder[] fieldBuilders) => fieldBuilders.Select(s=> Field.Create(s)).ToArray();


        public Field Copy()
        {
            var field = Create(this.Position);
            field.PutPieceOnField(_piece);
            return field;
        }

        public void PutPieceOnField(Piece piece) => _piece = piece;
        public void EmptyField() => _piece = null;
        public bool IsPiece() => _piece != null;
        public bool IsPiece(PieceType type) => IsPiece() && _piece.IsType(type);
        public bool IsPiece(ColorType color) => IsPiece() && _piece.IsColor(color);
        public bool IsPiece(PieceType type, ColorType color) => IsPiece(type) && IsPiece(color);

        public Piece GetPiece()
        {
            if(!IsPiece())
            {
                return null;
            }

            return _piece;
        }

        public FieldDto ShowField()
        {
            return new FieldDto
            {
                Position = Position,
                Piece = IsPiece() ? _piece.ShowPiece() : null
            };
        }
    }
}
