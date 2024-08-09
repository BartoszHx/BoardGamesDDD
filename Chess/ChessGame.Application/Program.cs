global using ChessGame.Application.Models.Enums;
global using ChessGame.Application.Models;
using ChessGame.Application.Behaviors;
using ChessGame.Application.Filters;
using ChessGame.Application.Queries.LoadChessGame;
using ChessGame.Domain;
using FluentValidation;
using System.Text.Json.Serialization;
using ChessGame.Infrastructure.Options;
using ChessGame.Application.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<HandlerValidationExceptionFilter>();
        options.Filters.Add<StreamNotFoundExceptionFilter>();
        options.Filters.Add<BusinessRuleValidationExceptionFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddCors(options =>
{
    var apiCors = builder.Configuration.GetSection("ApiCors").Get<ApiCorsOptions>();
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(apiCors.Origins)
              .WithMethods("GET", "POST")
              .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<LoadChessGameValidator>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddChessGames(builder.Configuration.GetSection("EventStorm").Get<EventStormOptions>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
