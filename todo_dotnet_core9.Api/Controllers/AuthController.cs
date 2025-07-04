using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using todo_dotnet_core9.Applications.Models;

namespace todo_dotnet_core9.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
            [HttpPost]
            public IActionResult Login([FromBody] UserViewModel user)
            {
                // Simulação de autenticação
                if (user.Username != "admin" || user.Password != "1234")
                    return Unauthorized("Usuário ou senha inválidos.");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("6WQ6J7Z3Q2GxCvTUlXGLrBD5Xf8Auh6qx0CeQ8qqVNs=");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }

    }
}