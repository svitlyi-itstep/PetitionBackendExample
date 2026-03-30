using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetitionBackend.Models;
using PetitionBackend.Services;
using System.Security.Claims;

namespace PetitionBackend.Controllers
{
    // ▶ Контролер API для роботи з даними про користувача

    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        public UserController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        // ▶ Закрита функція для отримання поточного авторизованого користувача.
        // Використовується ендпоінтами GET api/user та GET api/user/me
        private ActionResult GetCurrentUser()
        {
            string? nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int id = Convert.ToInt32(nameIdentifier);

            return Ok(_userService.GetUserById(id));
        }

        [Authorize]
        [HttpGet()]
        public ActionResult GetUser()
        {
            return GetCurrentUser();
        }

        [Authorize]
        [HttpGet("me")]
        public ActionResult GetMe()
        {
            return GetCurrentUser();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet("all")]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_userService.GetAll());
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet("paged/{page}")]
        public ActionResult<PagedResult<User>> GetUsersPaged(int page, int size = 2)
        {
            var users = _userService.GetAll();
            return Ok(new PagedResult<User>()
            {
                Items = users.GetRange((page - 1) * size, size),
                TotalCount = users.Count(),
                Page = page,
                PagesCount = users.Count() / size,
                PageSize = size
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddUser([FromBody] UserRegisterDTO user)
        {
            _userService.RegisterUser(user);
            return Ok();
        }

        // ▶ Ця функція призначена для реєстрації нового користувача
        // Вона допускає реєстрацію користувачів лише з роллю User
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] UserRegisterDTO user)
        {
            if (user.Role != UserRole.User)
                return BadRequest("Ви можете зареєструватися лише як користувач!");
            _userService.RegisterUser(user);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] UserLoginDTO credentials)
        {
            var user = _userService.ValidateUser(credentials);
            if (user == null)
                return Unauthorized();
            var (AccessToken, RefreshToken) = _tokenService.GenerateTokens(user);
            return Ok(new
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken
            });
        }

        [HttpPost("refresh")]
        public ActionResult RefreshSession([FromBody] string refreshToken)
        {
            var refreshSession = _tokenService.GetRefreshToken(refreshToken);
            if (refreshSession == null ||
                refreshSession.ExpirationDate < DateTime.UtcNow)
                return Unauthorized();
            var user = _userService.GetUserById(refreshSession.UserId);
            var (AccessToken, RefreshToken) = _tokenService.GenerateTokens(user);
            return Ok(new
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken
            });
        }
    }
}
