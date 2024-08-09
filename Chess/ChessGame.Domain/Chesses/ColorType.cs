namespace ChessGame.Domain.Chesses
{
    public enum ColorType
    {
        White,
        Black
    }

    public static class ColorTypeExtension
    {
        public static ColorType Opposite(this ColorType color)
        {
            return color == ColorType.White? ColorType.Black : ColorType.White;
        }
    }
}
