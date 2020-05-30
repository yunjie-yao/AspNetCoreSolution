using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YangXuAPI.DtoParameters;
using YangXuAPI.Models;

namespace YangXuAPI.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController:ControllerBase
    {
        private readonly TokenDtoParameter _tokenParameter;
        public AuthenticationController(IConfiguration configuration)
        {
            var tokenParameter =
                configuration.GetSection("TokenParameter").Get<TokenDtoParameter>();

            _tokenParameter = tokenParameter;
        }

        [HttpPost("{requesttoken}")]
        public ActionResult RequestToken(LoginDto login)
        {
            // 此处需要做用户名和密码的校验
            if (string.IsNullOrEmpty(login.UserName)||string.IsNullOrEmpty(login.Password))
            {
                return BadRequest();
            }

            // 生成Token和RefreshToken
            var token = GenerateToken(login.UserName, "test1");
            var refreshToken = "refreshToken";
            return Ok(new {token,refreshToken});
        }

        [HttpPost("refreshtoken")]
        public ActionResult RefreshToken(RefreshTokenDto refreshTokenRequest)
        {
            if (string.IsNullOrEmpty(refreshTokenRequest.Token)
                || string.IsNullOrEmpty(refreshTokenRequest.RefreshToken))
            {
                return BadRequest("Invalid RefreshTokenRequest");
            }

            var handler=new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal claim = handler.ValidateToken(
                    refreshTokenRequest.Token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenParameter.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false


                    },
                    out SecurityToken securityToken);

                var userName = claim.Identity.Name;

                var token = GenerateToken(userName, "test");
                var refreshToken = "refreshToken";

                return Ok(new {token, refreshToken});

            }
            catch (Exception)
            {
                return BadRequest("Invalid RefreshTokenRequest");
            }
        }

        private string GenerateToken(string userName, string roleName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, roleName),
                //new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds()}"),
                //new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(_tokenParameter.AccessExpiration)).ToUnixTimeMilliseconds()}"), 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.Secret));
            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer:_tokenParameter.Issuer,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenParameter.AccessExpiration),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }
    }
}
