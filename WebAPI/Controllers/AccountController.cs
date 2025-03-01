using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register()
    {
        return NoContent();
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        return NoContent();
    }

    [HttpPost("refresh")]
    public IActionResult Refresh()
    {
        return NoContent();
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Checking if refresh token exists, is valid, not used or revoked and belongs to user
        // Setting refresh token as revoked
        // Returning 204 No Content status code
        return NoContent();
    }
}