using ChessGame.Infrastructure.Common;
using EventStore.Client;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ChessGame.Infrastructure
{
    public class AggregateRepository<TA> : IAggregateRepository<TA> where TA : class, IAggregateRoot
    {
        private readonly EventStoreClient _eventStoreClient;
        private readonly string _stramBaseName;

        public AggregateRepository(EventStoreClient eventStoreClient)
        {
            _eventStoreClient = eventStoreClient;
            var aggregateType = typeof(TA);
            _stramBaseName = aggregateType.Name;
        }

        public async Task AppendAsync(TA aggregate, CancellationToken cancellationToken = default)
        {
            if (null == aggregate)
                throw new ArgumentNullException(nameof(aggregate));
            if (!aggregate.Events.Any())
                return;

            var streamName = GetStreamName(aggregate.Id);

            var eventList = aggregate.Events.Select(Map).ToArray();

            var result = await _eventStoreClient.AppendToStreamAsync(streamName, StreamState.Any,
                eventList.ToArray(), cancellationToken: cancellationToken);

            aggregate.ClearEvents();
        }

        public async Task<List<IDomainEvent>> ReadEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default)
        {
            var streamName = GetStreamName(aggregateId);
            var result = _eventStoreClient.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start);

            var events = new List<IDomainEvent>();

            foreach (var data in await result.ToListAsync(cancellationToken: cancellationToken))
            {
                var eventMetaData = JsonSerializer.Deserialize<EventMeta>(Encoding.UTF8.GetString(data.Event.Metadata.ToArray()));
                Type? typeInfo = Type.GetType(eventMetaData.EventType);
                if (typeInfo is null)
                {
                    throw new Exception($"Invalid type {eventMetaData.EventType}");
                }

                var jsonData = Encoding.UTF8.GetString(data.Event.Data.ToArray());
                var eventInfo = JsonConvert.DeserializeObject(jsonData, typeInfo, new JsonSerializerSettings()
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                });

                events.Add((IDomainEvent)eventInfo);
            }

            events = events.OrderBy(ob => ob.AggregateVersion).ToList();

            return events;
        }

        private string GetStreamName(Guid id) => $"{_stramBaseName}_{id}";

        private static EventData Map(IDomainEvent @event)
        {
            var meta = new EventMeta()
            {
                EventType = @event.GetType().AssemblyQualifiedName
            };

            var metaJson = System.Text.Json.JsonSerializer.Serialize(meta);
            var metadata = Encoding.UTF8.GetBytes(metaJson);

            var eventData = new EventData(
                Uuid.NewUuid(),
                @event.GetType().Name,
                JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                }),
                metadata
                );
            return eventData;
        }

        /// <summary>
        ///  Meta data information for an event which will also saved into each Event Payload
        /// </summary>
        internal struct EventMeta
        {
            public string EventType { get; set; }
        }
    }
}
