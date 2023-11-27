using Azure.Core.GeoJson;
using FGP_API.Authentification;
using FGP_API.Models;
using FGP_API.Utils.Helpers;
using FGP_API.Utils.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FGP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<FGPUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly TokenEncryptionService _tokenEncryptionService;
        private readonly ApplicationDbContext _applicationDbContext;
        public AuthenticationController(
            UserManager<FGPUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, 
            TokenEncryptionService tokenEncryptionService,
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenEncryptionService = tokenEncryptionService;
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Register User application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Username);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exist" });

            FGPUser user = new FGPUser()
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation has failed" }); // TODO : could fail if password is not long or respect compliance

            var userCreated = await _userManager.FindByNameAsync(model.Username);
            if (userCreated != null)
            { 
                using (var context = _applicationDbContext)
                { 
                    _applicationDbContext.FGPUsers.Add(user);
                    var  valur = _applicationDbContext.SaveChanges();
                }
            }   
         

            return Ok(new Response { Status = "Success", Message = "User created successfully " + userCreated.Id });
        }
   
        /// <summary>
        /// Register User application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Username);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exist" });

            FGPUser user = new FGPUser()
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation has failed" }); // TODO : could fail if password is not long or respect compliance

            if(! await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if(await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return Ok(new Response { Status = "Success", Message = "User created successfully" });
        }


        /// <summary>
        /// Login to application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
                user = await _userManager.FindByEmailAsync(model.Username);

            if ((user != null) && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                foreach (var userrole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userrole));
                } 
                //TODO : verify and look for a valid Token otherwise generate new one


                var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:IssuerSigningKey"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
                    );
                var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                // Save the token for the user
                await _userManager.SetAuthenticationTokenAsync(user, "FGPAPI", "WebAuthentication", _tokenEncryptionService.EncryptToken(tokenStr));

                return Ok(new
                {
                    token = tokenStr,
                    username = user.UserName,
                    expiry = DateTime.Now.AddHours(1)
                }); 
            }

            return Unauthorized();
        }

        
    }
}
