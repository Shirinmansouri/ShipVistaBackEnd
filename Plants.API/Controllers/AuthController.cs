using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Plants.API.Configuration;
using Plants.Domain.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly JwtConfig _jwtConfig;
        public AuthController(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        [HttpPost("Login")]
        public   AuthModelResponse Login([FromBody] AuthModelRequest  authModelRequest)
        {
            AuthModelResponse authModelResponse = new AuthModelResponse();

            //just For sample 
            if (authModelRequest.UserName.ToLower() == "test" && authModelRequest.Password.ToLower() == "test")
            {
                var jwtToken = GenerateJwtToken();
                authModelResponse.ToSuccess<AuthModelResponse>();
                authModelResponse.token = jwtToken;
                return authModelResponse;

            }
            else
                return authModelResponse.ToFailedAuthentication<AuthModelResponse>();

        }
        private string GenerateJwtToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
 
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.TokenExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
