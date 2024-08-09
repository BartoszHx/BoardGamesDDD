namespace ChessGame.Infrastructure.Common
{
    public interface IAggregateRepository<TA> where TA : class, IAggregateRoot
    {
        Task AppendAsync(TA aggregate, CancellationToken cancellationToken = default);

        Task<List<IDomainEvent>> ReadEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default);
    }
}
