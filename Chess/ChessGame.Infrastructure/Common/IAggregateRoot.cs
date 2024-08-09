namespace ChessGame.Infrastructure.Common
{
    public interface IAggregateRoot : IBaseEntity
    {
        long Version { get; }
        void ClearEvents();
        IReadOnlyCollection<IDomainEvent> Events { get; }
    }
}
