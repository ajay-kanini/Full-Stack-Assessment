using HospitalManagement.Interface;
using HospitalManagement.Models;
using HospitalManagement.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalManagement.Service
{
    public class GenerateTokenService : IGenerateToken
    {
     
        private readonly SymmetricSecurityKey _key;

        public GenerateTokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string? GenerateToken(UserDTO userDTO)
        {
            string token = string.Empty;
            //User identity
            if (userDTO.Role == null)
                throw new Exception("Role is null for token generation");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,userDTO.Id.ToString()),
                new Claim(ClaimTypes.Role,userDTO.Role)
            };
            //Signature algorithm
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            //Assembling the token details
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = cred
            };
            //Using the handler to generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var myToken = tokenHandler.CreateToken(tokenDescription);
            token = tokenHandler.WriteToken(myToken);
            return token;
        }

    }
    
}
