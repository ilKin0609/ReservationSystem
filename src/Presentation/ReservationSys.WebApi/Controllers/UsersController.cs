using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ReservationSys.Application.Features.User.Commands.Email.ChangePassword;
using ReservationSys.Application.Features.User.Commands.Email.ConfirmEmail;
using ReservationSys.Application.Features.User.Commands.Email.PasswordReset.ResetPassword;
using ReservationSys.Application.Features.User.Commands.Email.PasswordReset.SendResetEmail;
using ReservationSys.Application.Features.User.Commands.Login;
using ReservationSys.Application.Features.User.Commands.RefreshToken;
using ReservationSys.Application.Features.User.Commands.Register;
using ReservationSys.Application.Features.User.Queries.ResetConfirmEmail;

namespace ReservationSys.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UsersController : ControllerBase
{
    private IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> UserRegister([FromBody]UserRegisterCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> LoginWithPassword([FromBody] LoginWithPasswordCommandRequest request)
    {
        var response= await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
    {
        var request = new ConfirmEmailCommandRequest
        {
            Token = token,
            UserId = userId
        };
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }
    [HttpPost]
    public async Task<IActionResult> SendResetEmail([FromBody] SendResetEmailCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> ResetConfirmEmail([FromQuery] string email, [FromQuery] string token)
    {
        var request = new ResetConfirmEmailQueryRequest
        {
            Email = email,
            Token = token
        };
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

}
