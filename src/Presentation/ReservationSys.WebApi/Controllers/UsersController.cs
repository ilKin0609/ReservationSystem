using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservationSys.Application.Features.User.Commands.ConfirmOtp;
using ReservationSys.Application.Features.User.Commands.Login.LoginPassword;
using ReservationSys.Application.Features.User.Commands.Login.LoginSendOTP;
using ReservationSys.Application.Features.User.Commands.Register;

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
    public async Task<IActionResult> ConfirmOTP([FromBody] ConfirmOtpRequest request)
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
    public async Task<IActionResult> LoginSendOTP([FromBody] LoginSendOTPRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> LoginConfirmOTP([FromBody] ConfirmOtpRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }
}
