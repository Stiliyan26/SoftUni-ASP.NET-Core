using JwtAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IConfiguration _configuration;

        public AuthorizationController(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            IUserStore<IdentityUser> userStore)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailStore = (IUserEmailStore<IdentityUser>)userStore;
            _userStore = userStore;
        }

        [HttpPost("authorization/token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] GetTokenRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                //401 or 404
                return Unauthorized();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordValid)
            {
                //401 or 400
                return Unauthorized();
            }

            var resp = GenerateAuthorizationToken(user.Id, user.UserName);

            return Ok(resp);
        }


        private AuthorizationResponse GenerateAuthorizationToken(string userId, string userName)
        {
            var now = DateTime.UtcNow;
            var secret = _configuration.GetValue<string>("Secret");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var userClaims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimTypes.NameIdentifier, userId),
            };

            //userClaims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var expires = now.Add(TimeSpan.FromMinutes(60));

            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: userClaims,
                    expires: expires,
                    audience: "https://localhost:7000/",
                    issuer: "https://localhost:7000/",
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            //we don't know about thread safety of token handler

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var resp = new AuthorizationResponse
            {
                UserId = userId,
                AuthorizationToken = encodedJwt,
                RefreshToken = string.Empty
            };

            return resp;
        }
    }
}
