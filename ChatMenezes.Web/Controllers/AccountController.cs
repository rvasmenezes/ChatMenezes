using ChatMenezes.Domain.Aggregates.UserAgg.Entities;
using ChatMenezes.Domain.Aggregates.UserAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.UserAgg.Requests;
using ChatMenezes.Domain.Aggregates.UserAgg.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMenezes.Web.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        public readonly IUserServices _userServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(
            SignInManager<User> signInManager,
            IUserServices userServices,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userServices = userServices;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Register()
            => View();

        [HttpPost]
        [Route("[controller]/Register")]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _userServices.Create(request);

                if (!response.ValidationResult.IsValid)
                    return BadRequest(response.ValidationResult.Errors.FirstOrDefault().ErrorMessage);

                return Ok("/Account/RegisterSuccess");
            }

            return BadRequest("Fill in the fields correctly!");
        }

        [HttpGet]
        public IActionResult RegisterSuccess()
            => View();

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
            => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _userServices.DoLogin(request);

                if (!response.ValidationResult.IsValid)
                    return BadRequest(response.ValidationResult.Errors.FirstOrDefault().ErrorMessage);

                var loginResponse = response.Entity as LoginResponse;

                _httpContextAccessor.HttpContext.Session.SetString("UserName", loginResponse.UserName);

                return Ok(loginResponse.Rota);
            }

            return BadRequest("Invalid login!");
        }
    }
}
