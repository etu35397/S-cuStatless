using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Stateless_Token.Model;

namespace Stateless_Token.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class JwtController : ControllerBase
    {
        private readonly JwtIssuerOptions _jwtOptions;
        public JwtController(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]DTO.LoginModel login)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var repository = new Stateless_Token.Model.AutenticationRepository();
            Model.User userFound = repository.GetUsers().FirstOrDefault(user => user.Username==login.Username && user.Password == login.Password);

            if(userFound==null)
                return Unauthorized();

            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, userFound.Username),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                ClaimValueTypes.Integer64),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            return Ok(response);
        }

        private static long ToUnixEpochDate(DateTime date)
        =>(long)Math.Round((date.ToUniversalTime() -
        new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
        .TotalSeconds);
    }
}
