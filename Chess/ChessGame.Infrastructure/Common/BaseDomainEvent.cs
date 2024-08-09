namespace ChessGame.Infrastructure.Common
{
    public record BaseDomainEvent<TA> : IDomainEvent where TA : IAggregateRoot
    {
        public long AggregateVersion { get; init; }
        
        public DateTime TimeStamp { get; init; }

        public Guid AggregateId { get; init; }

        protected BaseDomainEvent() { }

        public BaseDomainEvent(TA aggregateRoot)
        {
            if (aggregateRoot is null)
            {
                throw new ArgumentNullException(nameof(aggregateRoot));
            }

            AggregateId = aggregateRoot.Id;
            AggregateVersion = aggregateRoot.Version;
            TimeStamp = DateTime.Now;
        }
    }
}
