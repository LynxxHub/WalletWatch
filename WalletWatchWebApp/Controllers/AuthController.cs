using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WalletWatchWebApp.Models.ViewModels;
using WalletWatchWebApp.Utils;

namespace WalletWatchWebApp.Controllers
{
    public class AuthController : Controller
    {
        public readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault();

                return View();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync("https://localhost:7234/api/auth/login", input);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<JWTResponse>();
                var jwt = tokenResponse?.Token;
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = input.RememberMe ? DateTime.UtcNow.AddHours(24) : DateTime.UtcNow.AddMinutes(15),
                };

                Response.Cookies.Append("JWT", jwt, cookieOptions);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, input.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties { IsPersistent = input.RememberMe, ExpiresUtc = input.RememberMe ? DateTimeOffset.UtcNow.AddHours(24) : DateTimeOffset.UtcNow.AddMinutes(15) });

                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = await response.Content.ReadAsStringAsync();

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync("https://localhost:7234/api/auth/register", input);

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Registration successful! Please check your email to confirm your account.";
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return Redirect("/");
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7234/api/auth/confirmEmail?userId={userId}&code={code}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Email confirmed successfully!";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["Notification"] = "Error confirming your email. Please try again.";
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout(int id)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete("JWT");

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete("JWT");

            return RedirectToAction("Login", "Auth");
        }
    }
}
