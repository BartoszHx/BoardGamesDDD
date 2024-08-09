using ChessGame.Infrastructure.Common;
using ChessGame.Infrastructure.Options;
using EventStore.Client;
using Microsoft.Extensions.DependencyInjection;

namespace ChessGame.Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services, EventStormOptions options)
        {
            var settings = EventStoreClientSettings
                .Create(options.ConnectionString);

            var client = new EventStoreClient(settings);
            services.AddSingleton(client);

            return services;
        }

        public static IServiceCollection AddEventsRepository<TA>(this IServiceCollection services) where TA : class, IAggregateRoot
        {
            return services.AddSingleton(typeof(IAggregateRepository<TA>), typeof(AggregateRepository<TA>));
        }
    }
}
