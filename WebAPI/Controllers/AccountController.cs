using DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequestDto req)
    {
        // Creating new user object
        // Hashing password
        // Saving user to database
        // Returning 201 Created status code

        return NoContent();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto req)
    {
        // Checking if user exists
        // Matching password hashes
        // Updating last login date
        // Generating JWT token
        // Generating refresh token
        // Saving refresh token to database
        // Returning JWT token and refresh token (200)

        return NoContent();
    }

    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshTokenRequestDto req)
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
    public IActionResult Logout(RefreshTokenRequestDto req)
    {
        // Checking if refresh token exists, is valid, not user or revoked and belongs to user
        // Setting refresh token as revoked
        // Returning 204 No Content status code
        return NoContent();
    }
}