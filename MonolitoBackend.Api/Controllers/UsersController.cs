using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Entidade;
using MonolitoBackend.Core.Interfaces;

namespace MonolitoBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHashService _hashService;

        public UsersController(IUserService userService, IPasswordHashService hashService)
        {
            _userService = userService;
            _hashService = hashService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto model)
        {
            if (await _userService.UserExistsByEmail(model.Email))
                return BadRequest("Email já está em uso");

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = _hashService.HashPassword(model.Password),
                Roles = new List<string> { "User" }
            };

            await _userService.CreateUser(user);

            return Ok(new { user.Id, user.UserName, user.Email });
        }
    }
}
