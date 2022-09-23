using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PresonelManagmentBE.Data;
using PresonelManagmentBE.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PresonelManagmentBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        
        public AuthenticateController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
             
                var token = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userRole = userRoles,
                    userId = user.Id
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
           var userExists = await _userManager.FindByNameAsync(model.Username);
           if(userExists != null)
           {
               return StatusCode(StatusCodes.Status500InternalServerError, new Response {Status = "Error", Message = "User already exists"});
           }

           var categories = _context.Categories.ToList();
           ApplicationUser user = new()
           {
               Email = model.Email,
               UserName = model.Username,
               SecurityStamp = Guid.NewGuid().ToString(),
               FirstName = "imie",
               LastName = "nazwisko",
                PhoneNumber = "123456789",
                Category = categories[1]
                
           };
           var result = await _userManager.CreateAsync(user, model.Password);
           return !result.Succeeded ? StatusCode(StatusCodes.Status500InternalServerError, new Response {Status = "Error", Message = "User creation failed"}) : Ok(new Response{Status = "Success", Message = "User created successfully"});
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}