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
        // Checking if refresh token exists, is valid, not user or revoked and belongs to user
        // Setting refresh token as used
        // Generating new refresh token
        // Saving it to database
        // Generating new JWT token
        // Returning new JWT token and refresh token (200)
        return NoContent();
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Checking if refresh token exists, is valid, not user or revoked and belongs to user
        // Setting refresh token as revoked
        // Returning 204 No Content status code
        return NoContent();
    }
}