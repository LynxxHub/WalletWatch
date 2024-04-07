using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text;
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
        private readonly IEmailSender _emailSender;
        public AuthController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IMapper mapper, 
            ITokenService tokenService, 
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _emailSender = emailSender;

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


            var user = _mapper.Map<User>(input);
            var result = await _userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var webAppBaseUrl = "https://localhost:7142";
                var emailConfirmationUrl = $"{webAppBaseUrl}/Auth/ConfirmEmail?userId={userId}&code={Uri.EscapeDataString(code)}";

                await _emailSender.SendEmailAsync(input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(emailConfirmationUrl)}'>clicking here</a>.");
                return Ok(new { Message = "Registration successful" });
            }

            return BadRequest();

        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("User ID and Code are required");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Invalid user ID");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully");
            }
            else
            {
                return BadRequest("Error confirming email");
            }
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
                return BadRequest($"User with username {input.Username} does not exist.");
            }
            var result = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, true);

            if (result.Succeeded)
            {
                var token = _tokenService.GenerateJwtToken(user, input.RememberMe);
                return Ok(new { token });
            }

            if (result.IsNotAllowed)
            {
                return Unauthorized("Account is not confirmed!");
            }

            return Unauthorized("Wrong username or password");
        }
    }
}
