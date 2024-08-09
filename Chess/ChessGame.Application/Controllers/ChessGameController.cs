using ChessGame.Application.Commands.CreateChessStandardGame;
using ChessGame.Application.Commands.DoMovement;
using ChessGame.Application.Queries.LoadChessGame;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChessGame.Application.Controllers;

[ApiController]
[Route("api/chess")]
public class ChessGameController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChessGameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-standard-game")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpValidationProblemDetails))]
    public async Task<ActionResult> PostCreateStandardGame()
    {
        var result = await _mediator.Send(new CreateChessStandardGameCommand());
        return Ok(result.ChessId);
    }

    [HttpGet("game")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChessDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> GetLoadChessGame(Guid id)
    {
        var result = await _mediator.Send(new LoadChessGameQuery { ChessId = id });
        return Ok(result.Chess);
    }

    [HttpPost("do-movement")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> GetDoMovement(DoMovementCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}
