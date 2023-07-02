using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snipe.Web.Services;
using Snipe.App.Features.Users.Models;
using Snipe.App.Features.Users.Services;

namespace Snipe.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserInfoProvider _userInfo;
        private readonly ISignInService _signInService;
        private readonly IUsersService _usersService;
        private readonly IEmailVerificationSender _emailVerificationSender;

        public AuthController(
            IUserInfoProvider accountInfo, 
            ISignInService signInService,
            IUsersService usersService,
            IEmailVerificationSender emailVerificationSender)
        {
            _userInfo = accountInfo;
            _signInService = signInService;
            _usersService = usersService;
            _emailVerificationSender = emailVerificationSender;
        }

        [HttpPost("sign-in")]
        public async Task<UserSignInResponse> SignIn(UserSignInRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            var signInResult = await _signInService.SignInAsync(request.Email, request.Password, cancellationToken);

            if (signInResult.Success && signInResult.UserId.HasValue)
            {
                return new UserSignInResponse
                {
                    Success = true,
                    AccessToken = signInResult.AccessToken,
                    RefreshToken = signInResult.RefreshToken,
                };
            }

            return new UserSignInResponse
            {
                Success = false,
                FailReason = signInResult.FailReason,
                LockoutExpiration = signInResult.LockoutExpiration,
            };
        }

        [HttpPost("sign-up")]
        public async Task<UserSignUpResponse> SignUp(UserSignUpRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            await _usersService.CreateUserAsync(request.Email, request.Password, request.VerificationCode, cancellationToken);

            return new UserSignUpResponse
            {
                Success = true,
            };
        }

        [HttpPost("send-email-verification-code")]
        public async Task<string> SendEmailVerificationCode(SendEmailVerificationCodeRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            return _emailVerificationSender.SendEmailVerificationCode(request.Email);
        }

        [HttpPost("refresh-token")]
        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            var signInResult = await _signInService.SignInWithRefreshTokenAsync(request.RefreshToken, cancellationToken);

            if (signInResult.Success && signInResult.AccessToken != null && signInResult.RefreshToken != null)
            {
                return new RefreshTokenResponse
                {
                    Success = true,
                    AccessToken = signInResult.AccessToken,
                    RefreshToken = signInResult.RefreshToken,
                };
            }

            return new RefreshTokenResponse
            {
                Success = false,
            };
        }

        [HttpGet("user")]
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

    public class UserSignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
    }

    public class UserSignUpResponse
    {
        public bool Success { get; set; }
    }

    public class SendEmailVerificationCodeRequest
    {
        public string Email { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenResponse
    {
        public bool Success { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
