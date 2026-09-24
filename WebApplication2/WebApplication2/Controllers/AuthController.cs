using WebApplication2.Dtos;

namespace WebApplication2.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

[ApiController]
[Route("api/auth")]                     // all actions here start with /api/auth
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST /api/auth/register
    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(AuthRequestDto request)
    {
        var user = await _authService.RegisterAsync(request.Username, request.Password);

        // null means "username taken". Same JSON shape as your other errors.
        if (user is null)
        {
            return Conflict(new { status = 409, error = "Username is already taken." });
        }

        // Copy only the safe fields into the response DTO.
        var response = new UserResponseDto { Id = user.Id, Username = user.Username };

        // 201 Created. There is no "get user by id" endpoint yet,
        // so we skip the Location header for now.
        return StatusCode(StatusCodes.Status201Created, response);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDto>> Login(AuthRequestDto request)
    {
        var user = await _authService.ValidateCredentialsAsync(request.Username, request.Password);

        // null covers BOTH "no such user" and "wrong password".
        // We deliberately give the same answer for both.
        if (user is null)
        {
            return Unauthorized(new { status = 401, error = "Invalid username or password." });
        }

        return Ok(new UserResponseDto { Id = user.Id, Username = user.Username });
    }
}