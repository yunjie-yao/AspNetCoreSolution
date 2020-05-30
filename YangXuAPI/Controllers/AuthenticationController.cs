﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private readonly TokenDtoParameter _tokenParameter=new TokenDtoParameter();
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
            var token = GenerateToken(login.UserName, "test");
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
            catch (Exception e)
            {
                return BadRequest("Invalid RefreshTokenRequest");
            }

            return Ok();
        }

        private string GenerateToken(string userName, string roleName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, roleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.Secret));
            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                _tokenParameter.Issuer,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenParameter.AccessExpiration),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }
    }
}
