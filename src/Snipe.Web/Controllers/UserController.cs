using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snipe.Web.Services;
using Snipe.App.Features.Users.Models;
using Snipe.App.Features.Users.Services;

namespace Snipe.Web.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserInfoProvider _userInfo;
        private readonly UsersService _usersService;

        public UserController(IUserInfoProvider accountInfo, UsersService usersService)
        {
            _userInfo = accountInfo;
            _usersService = usersService;
        }

        [HttpPost("sign-in")]
        public async Task<UserSignInResponse> SignIn(UserSignInRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            var signInResult = await _usersService.SignIn(request.Email, request.Password, cancellationToken);

            if (signInResult.Success && signInResult.UserId.HasValue)
            {
                return new UserSignInResponse
                {
                    Success = true,
                };
            }

            return new UserSignInResponse
            {
                Success = false,
                FailReason = signInResult.FailReason,
                LockoutExpiration = signInResult.LockoutExpiration,
            };
        }

        [HttpGet]
        [Authorize]
        public UserInfoDto GetUserInfo()
        {
            return new UserInfoDto
            {
                UserId = _userInfo.UserId,
                Username = _userInfo.Username,
                Role = _userInfo.Role
            };
        }
    }

    public class UserInfoDto
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public class UserSignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserSignInResponse
    {
        public bool Success { get; set; }
        public SignInFailReason? FailReason { get; set; }
        public DateTime? LockoutExpiration { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
