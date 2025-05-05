using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Interfaces;

namespace MonolitoBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHashService _hashService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, IPasswordHashService hashService, ITokenService tokenService)
        {
            _userService = userService;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userService.GetUserByUserName(model.UserName);
            if (user == null || !_hashService.VerifyPassword(model.Password, user.Password))
                return Unauthorized("Usuário ou senha inválidos");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
