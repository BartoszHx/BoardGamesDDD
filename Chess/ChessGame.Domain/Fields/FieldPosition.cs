namespace ChessGame.Domain.Fields
{
    public enum FieldPosition
    {
        A1 = 1,
        B1 = 2,
        C1 = 3,
        D1 = 4,
        E1 = 5,
        F1 = 6,
        G1 = 7,
        H1 = 8,
        A2 = 9,
        B2 = 10,
        C2 = 11,
        D2 = 12,
        E2 = 13,
        F2 = 14,
        G2 = 15,
        H2 = 16,
        A3 = 17,
        B3 = 18,
        C3 = 19,
        D3 = 20,
        E3 = 21,
        F3 = 22,
        G3 = 23,
        H3 = 24,
        A4 = 25,
        B4 = 26,
        C4 = 27,
        D4 = 28,
        E4 = 29,
        F4 = 30,
        G4 = 31,
        H4 = 32,
        A5 = 33,
        B5 = 34,
        C5 = 35,
        D5 = 36,
        E5 = 37,
        F5 = 38,
        G5 = 39,
        H5 = 40,
        A6 = 41,
        B6 = 42,
        C6 = 43,
        D6 = 44,
        E6 = 45,
        F6 = 46,
        G6 = 47,
        H6 = 48,
        A7 = 49,
        B7 = 50,
        C7 = 51,
        D7 = 52,
        E7 = 53,
        F7 = 54,
        G7 = 55,
        H7 = 56,
        A8 = 57,
        B8 = 58,
        C8 = 59,
        D8 = 60,
        E8 = 61,
        F8 = 62,
        G8 = 63,
        H8 = 64,
    }

    public static class FieldPositionHelper
    {
        private static FieldPosition[] _leftEdges = new FieldPosition[] { FieldPosition.A1, FieldPosition.A2, FieldPosition.A3, FieldPosition.A4, FieldPosition.A5, FieldPosition.A6, FieldPosition.A7, FieldPosition.A8 };
        private static FieldPosition[] _rightEdges = new FieldPosition[] { FieldPosition.H1, FieldPosition.H2, FieldPosition.H3, FieldPosition.H4, FieldPosition.H5, FieldPosition.H6, FieldPosition.H7, FieldPosition.H8 };

        public static bool IsIn(this FieldPosition source, params FieldPosition[] enums)
        {
            return enums.Any(e => e == source);
        }

        public static bool IsBetween(this FieldPosition source, FieldPosition start, FieldPosition end)
        {
            return source >= start && source <= end;
        }

        public static FieldPosition? TakeUp(this FieldPosition source) => TakeHelper(source, 8);
        public static FieldPosition? TakeDown(this FieldPosition source) => TakeHelper(source, -8);
        public static FieldPosition? TakeLeft(this FieldPosition source) => TakeHelper(source, -1, _rightEdges);
        public static FieldPosition? TakeRight(this FieldPosition source) => TakeHelper(source, 1, _leftEdges);
        public static FieldPosition? TakeUpLeft(this FieldPosition source) => TakeHelper(source, 7, _rightEdges);
        public static FieldPosition? TakeUpRight(this FieldPosition source) => TakeHelper(source, 9, _leftEdges);
        public static FieldPosition? TakeDownLeft(this FieldPosition source) => TakeHelper(source, -9, _rightEdges);
        public static FieldPosition? TakeDownRight(this FieldPosition source) => TakeHelper(source, -7, _leftEdges);

        public static bool IsEighthRankField(this FieldPosition source)
        {
            return source.IsBetween(FieldPosition.A1, FieldPosition.H1) || source.IsBetween(FieldPosition.A8, FieldPosition.H8);
        }

        private static FieldPosition? TakeHelper(FieldPosition source, int number)
        {
            try
            {
                var position = source + number;
                if(position <= 0 || (int)position > 64)
                {
                    return null;
                }

                return position;
            }
            catch
            {
                return null;
            }
        }

        private static FieldPosition? TakeHelper(FieldPosition source, int number, FieldPosition[] edges)
        {
            FieldPosition? take = TakeHelper(source, number);

            if(!take.HasValue)
            {
                return null;
            }

            if(edges.Contains(take.Value)) 
            {
                return null;
            }

            return take;
        }
    }
}
