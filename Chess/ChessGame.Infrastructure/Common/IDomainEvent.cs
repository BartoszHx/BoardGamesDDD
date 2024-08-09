namespace ChessGame.Infrastructure.Common
{
    public interface IDomainEvent
    {
        public long AggregateVersion { get; }
        Guid AggregateId { get; }
        DateTime TimeStamp { get; }
    }
}
