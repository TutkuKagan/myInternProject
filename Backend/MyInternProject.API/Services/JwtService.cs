using System.Text;

namespace MyInternProject.API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }


        public string GenerateToken(Guid id, string username)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:Expires"]));

        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Iss, issuer),
            new Claim(JwtRegisteredClaimNames.Aud, audience),
        };

        var token = new JwtSecurityToken(issuer,audience,claims,expires: expires,
        signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        ));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }




}