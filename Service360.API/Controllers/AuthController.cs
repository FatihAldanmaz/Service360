using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Service360.Application.DTOs.Auth;
using Service360.Domain.Entities;


namespace Service360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if(existingUser is not null)
            {
                return BadRequest("Bu e-posta adresi zaten kayıtlı.");
            }

            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                CompanyId = Guid.Empty
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) 
            {
                return BadRequest(result.Errors);
            }

            return Ok("Kullanıcı başarıyla oluşturuldu.");
        }
    }
}
