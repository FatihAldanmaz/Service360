using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Service360.Application.DTOs.Auth;
using Service360.Domain.Entities;
using Service360.Application.Interfaces;
using Service360.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Service360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IJwtTokenService jwtTokenService, AppDbContext context)
        {
            _userManager = userManager; 
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        [HttpPost("company-register")]
        public async Task<IActionResult> CompanyRegister(
    CompanyRegisterRequest request)
        {

            var company = new Company
            {
                Name = request.CompanyName,
                TaxNumber = request.TaxNumber
            };

            await _context.Companies.AddAsync(company);

            await _context.SaveChangesAsync();


            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                CompanyId = company.Id
            };


            var result = await _userManager.CreateAsync(
                user,
                request.Password);


            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            const string adminRole = "Admin";

            if (!await _roleManager.RoleExistsAsync(adminRole))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>
                {
                    Name = adminRole
                });
            }

            await _userManager.AddToRoleAsync(user, adminRole);

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateToken(user, roles);


            return Ok(new
            {
                Message = "Firma ve yönetici hesabı başarıyla oluşturuldu.",
                Token = token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            { 
                return Unauthorized("E-posta veya şifre hatalı.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid) 
            {
                return Unauthorized("E-posta veya şifre hatalı.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateToken(user, roles);

            return Ok(token);
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var companyId = User.FindFirst("companyId")?.Value;

            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            return Ok(new
            {
                UserId = userId,
                Email = email,
                CompanyId = companyId,
                Roles = roles
            });
        }
    }
}
