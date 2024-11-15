using CommerceAPI_IOON.CommerceAPI.DTOs;
using CommerceAPI_IOON.CommerceAPI.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CommerceAPI_IOON.CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommerceController : ControllerBase
    {
        private readonly CommerceService _commerceService;

        public CommerceController(CommerceService commerceService)
        {
            _commerceService = commerceService;
        }

        // POST api/commerce/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterCommerceAndUser([FromBody] RegisterRequestDto request)
        {
            if (string.IsNullOrEmpty(request.CommerceName) || string.IsNullOrEmpty(request.UserName) ||
                string.IsNullOrEmpty(request.UserEmail) || string.IsNullOrEmpty(request.UserPassword))
            {
                return BadRequest("All fields are required.");
            }

            try
            {
                await _commerceService.RegisterCommerceAndUserAsync(
                    request.CommerceName,
                    request.CommerceAddress,
                    request.RUC,
                    request.UserName,
                    request.UserEmail,
                    request.UserPassword
                );

                return Ok("Commerce and user successfully registered.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUserAndCommerce(Guid userId)
        {
            try
            {
                await _commerceService.DeleteUserAndCommerceAsync(userId);
                return Ok("User and associated commerce deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
