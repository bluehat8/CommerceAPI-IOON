using CommerceAPI_IOON.CommerceAPI.DTOs;
using CommerceAPI_IOON.CommerceAPI.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtAuthService _jwtService;

    public AuthController(JwtAuthService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var token = await _jwtService.GenerateJwtTokenAsync(request.Username, request.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message); // Devuelves el mensaje de error si el usuario no es válido o está inactivo
        }
    }
}
