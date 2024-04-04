using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletWatchAPI.DTOs;
using WalletWatchAPI.Models;
using WalletWatchAPI.Services.Interfaces;

namespace WalletWatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }


        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(new { users });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //TODO:implement email confirmation

            var user = _mapper.Map<User>(input);
            var result = await _userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Registration successful" });
            }

            return BadRequest(result.Errors);

        }
            
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(input.Username);

            if (user == null)
            {
                ModelState.AddModelError("", $"User with username {input.Username} does not exists.");
                return BadRequest(ModelState);
            }
            var result = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, true);

            if (result.Succeeded)
            {
                var token = _tokenService.GenerateJwtToken(user, input.RememberMe);
                return Ok(new { token });
            }
            ModelState.AddModelError("", "Wrong login or password");
            return BadRequest(ModelState);
        }
    }
}
