using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SMECService.Models;
using SMECService.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SMECService.Controllers
{
    [Route("api/[controller]")]
    public class TokenController: Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public TokenController(SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Jwt([FromBody]TokenRequestViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null) return new StatusCodeResult(500);

            switch (model.grant_type)
            {
                case "password": 
                    return await GetToken(model);
                default:
                    // not supported - return a HTTP 401 (Unauthorized)
                    return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> GetToken(TokenRequestViewModel model)
        {
            try
            {
                // check if there's an user with the given username

                var user = await _signInManager.UserManager.FindByNameAsync(model.username);

                // fallback to support e-mail address instead of username

                if (user == null && model.username.Contains("@"))
                    user = await _signInManager.UserManager.FindByEmailAsync(model.username);
                if (user == null || !await _signInManager.UserManager.CheckPasswordAsync(user, model.password))
                {
                    return new UnauthorizedResult();
                }

                // username & password matches: create and return the

                DateTime now = DateTime.UtcNow;

                // add the registered claims for JWT (RFC7519).
                // For more info, see https://tools.ietf.org/html/rfc7519#section-4.1

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
                };

                var tokenExpirationMins = _configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
                var issuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Auth:Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Auth:Jwt:Issuer"],
                    audience: _configuration["Auth:Jwt:Audience"],
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                    signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                // build & return the response
                var response = new TokenResponseViewModel()
                {
                    token = encodedToken,
                    expiration = tokenExpirationMins
                };
                return Json(response);
            }
            catch (Exception e)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
