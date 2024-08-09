using System.Collections.Immutable;

namespace ChessGame.Infrastructure.Common
{
    public abstract class BaseAggregateRoot : IBaseEntity, IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public long Version { get; protected set; }

        private List<IDomainEvent> _events = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> Events => _events.ToImmutableArray();

        protected void AddEvent(IDomainEvent @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            _events.Add(@event);
            Version++;
        }

        protected abstract void Apply(IDomainEvent @event);

        public void ClearEvents()
        {
            _events.Clear();
        }

        protected void Rebuild(IEnumerable<IDomainEvent> events)
        {
            foreach (var eve in events)
            {
                Apply(eve);
            }

            Version++;
            ClearEvents();
        }
    }
}
