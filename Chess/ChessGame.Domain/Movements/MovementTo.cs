using ChessGame.Domain.Fields;

namespace ChessGame.Domain.Movements
{
    public class MovementTo
    {
        public FieldPosition FromPosition { get; init; }
        public FieldPosition ToPosition { get; init; }
        public MovementType Movement { get; init; }

        private MovementTo()
        {

        }

        internal static MovementTo Create(FieldPosition fromPosition, FieldPosition toPosition, MovementType movement)
        {
            return new MovementTo
            {
                FromPosition = fromPosition,
                ToPosition = toPosition,
                Movement = movement
            };
        }

    }
    public static class MovementToExtension
    {
        public static void AddMovement(this List<MovementTo> source, FieldPosition fromPosition, FieldPosition toPosition, MovementType movement = MovementType.Normal)
        {
            source.Add(MovementTo.Create(fromPosition, toPosition, movement));
        }

        public static void AddMovement(this List<MovementTo> source, FieldPosition? fromPosition, FieldPosition? toPosition, MovementType movement = MovementType.Normal)
        {
            if (!toPosition.HasValue || !fromPosition.HasValue)
            {
                return;
            }

            source.AddMovement(fromPosition.Value, toPosition.Value, movement);
        }

        public static FieldPosition[] ToFieldPosition(this MovementTo[] source)
        {
            return source.Select(s => s.ToPosition).ToArray();
        }
    }
}
