using Application.UseCases.Commands.Users.Login;
using Application.UseCases.Commands.Users.Logout;
using Application.UseCases.Commands.Users.Refresh;
using Application.UseCases.Commands.Users.Register;
using Application.UseCases.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result);
        return CreatedAtAction(nameof(Register), result.Data);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result);
        return Ok(result.Data);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result);
        return Ok(result.Data);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutUserCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var query = new CurrentLoggedInUserQuery();
        
        var userInfo = await _mediator.Send(query, cancellationToken);
        if (userInfo == null) return Unauthorized();
        return Ok(userInfo);
    }
}