using Braintree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrganisationSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Claims;
using OrganisationSystem.Models.Enums;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.InfrastructureLayer;
using OrganisationSystem.APILayer;


namespace OrganisationSystem.ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController(JwtOptions jwtOptions , Context context) : ControllerBase
    {

        PasswordHasher<object> hasher = new PasswordHasher<object>();


        [HttpPost]
        public ActionResult SignUp(UserDto userDto)
        {
            var user = new User
            {
                  Name = userDto.Name
                , Password = hasher.HashPassword(null, userDto.Password)
                , Role = userDto.Role

            };

            context.users.Add(user);
            context.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public ActionResult LogIn(UserDto userDto)
        {
            var user = context.users.FirstOrDefault(x => x.Name == userDto.Name );

            if (user == null)
            {
                return Unauthorized();
            }

            var result = hasher.VerifyHashedPassword(null, user.Password, userDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized();
            }
            
                var jwthandler = new JwtSecurityTokenHandler();
                var tokendescription = new SecurityTokenDescriptor
                {
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)), SecurityAlgorithms.HmacSha256),
                    Issuer = jwtOptions.Issuer,
                    Audience = jwtOptions.Audience,
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new(ClaimTypes.Name , user.Name),
                        new(ClaimTypes.Role , user.Role.ToString())
                    })

                };

                var securityToken = jwthandler.CreateToken(tokendescription);
                var accessToken = jwthandler.WriteToken(securityToken);

                return Ok(accessToken);
            
        }
    }
}
