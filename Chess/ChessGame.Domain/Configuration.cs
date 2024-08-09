using ChessGame.Domain.Chesses;
using ChessGame.Infrastructure;
using ChessGame.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;

namespace ChessGame.Domain
{
    public static class Configuration
    {
        public static IServiceCollection AddChessGames(this IServiceCollection services, EventStormOptions options)
        {
            services.AddEventStore(options);
            services.AddEventsRepository<Chess>();

            return services;
        }
    }
}
