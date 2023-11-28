using FGP_API.Models.Authentification;
using FGP_API.Models.DAO;
using FGP_API.Models.Enum;
using FGP_API.Models.ResponseModels;
using FGP_API.Utils.Helpers;
using FGP_API.Utils.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FGP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        UserManager<UserApplication> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        TokenEncryptionService tokenEncryptionService,
        ApplicationDbContext applicationDbContext,
        ILogger<AuthenticationController> logger) : ControllerBase
    {
        #region Attributes and Constructeur 
        private readonly UserManager<UserApplication> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly TokenEncryptionService _tokenEncryptionService = tokenEncryptionService;
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly ILogger<AuthenticationController> _logger = logger;
        #endregion

        #region Authentication Api's



        /// <summary>
        /// Register FGP User 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">User created successfully</response>
        /// <response code="404">User already exists</response>
        /// <response code="500">Oops ! User has missing/invalid values or  Can't create your product right now</response>
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(RegistrationResponseModel), 200)]
        [ProducesResponseType(typeof(RegistrationResponseModel), 404)]
        [ProducesResponseType(typeof(RegistrationResponseModel), 500)]
        public async Task<IActionResult> Register([FromBody] RegisterationModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName) != null || await _userManager.FindByEmailAsync(model.Email) != null;
            if (userExists)
            {
                _logger.LogWarning("User registration failed - User already exists.");
                return ConflictResponse("The requested user already exists in the system.", ["User already exists"]);
            } 

            var user = CreateUserFromModel(model);
            var userApplication = CreateUserApplicationFromModel(model);

            var result = await _userManager.CreateAsync(userApplication, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToArray();
                _logger.LogError("User registration failed - Invalid information.", errors);
                return ErrorResponse("Failed to create user due to invalid information. Please check your details and try again.", errors);
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.BasicUser))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.BasicUser));

            _logger.LogInformation("Admin User registration  - Adding User Roles to user");
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(userApplication, UserRoles.BasicUser);
            try
            {
                userApplication = await _userManager.FindByNameAsync(model.UserName);
                if (userApplication != null)
                {
                    user.UserApplicationId = userApplication.Id;
                }
                await SaveUserAppAsync(user);
            }
            catch (Exception ex) when (ex is DbUpdateException || ex is not null)
            {
                _logger.LogError(ex, "User registration failed - Unexpected error while creating the user.");
                return ErrorResponse("Unexpected error occurred while creating the user. Please try again later or contact support.", [ex.Message]);
            }

            _logger.LogInformation("User registered successfully / Username: " + user.UserName);
            return Ok(new RegistrationResponseModel
            {
                Status = ApiResponseStatus.Success.GetDescription(),
                Message = "Congratulations! You're all set. Your account has been created.",
                Username = user.UserName
            });
        }


        /// <summary>
        /// Register Admin FGP User 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">User created successfully</response>
        /// <response code="404">User already exists</response>
        /// <response code="500">Oops ! User has missing/invalid values or  Can't create your product right now</response>
        [HttpPost]
        [Route("RegisterAdmin")]
        [ProducesResponseType(typeof(RegistrationResponseModel), 200)]
        [ProducesResponseType(typeof(RegistrationResponseModel), 404)]
        [ProducesResponseType(typeof(RegistrationResponseModel), 500)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterationModel model)
        {

            var userExists = await _userManager.FindByNameAsync(model.UserName) != null || await _userManager.FindByEmailAsync(model.Email) != null;
            if (userExists)
            {
                _logger.LogWarning("User registration failed - User already exists.");
                return ConflictResponse("The requested user already exists in the system.", ["User already exists"]);
            }

            var user = CreateUserFromModel(model);
            var userApplication = CreateUserApplicationFromModel(model);

            var result = await _userManager.CreateAsync(userApplication, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToArray();
                _logger.LogError("User registration failed - Invalid information.", errors);
                return ErrorResponse("Failed to create user due to invalid information. Please check your details and try again.", errors);
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            _logger.LogInformation("Admin User registration  - Adding Admin Roles to user");
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(userApplication, UserRoles.Admin);

            try
            {
                userApplication = await _userManager.FindByNameAsync(model.UserName);
                if (userApplication != null)
                {
                    user.UserApplicationId = userApplication.Id;
                }
                await SaveUserAppAsync(user);
            }
            catch (Exception ex) when (ex is DbUpdateException || ex is not null)
            {
                _logger.LogError(ex, "User registration failed - Unexpected error while creating the user.");
                return ErrorResponse("Unexpected error occurred while creating the user. Please try again later or contact support.", [ex.Message]);
            }

            _logger.LogInformation("User registered successfully / Username: " + user.UserName);
            return Ok(new RegistrationResponseModel
            {
                Status = ApiResponseStatus.Success.GetDescription(),
                Message = "Congratulations! You're all set. Your account has been created.",
                Username = user.UserName
            });
        }



        /// <summary>
        /// Register Admin FGP User 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">User logged in successfully / (Decrypted)Token returned in data .</response>
        /// <response code="401">User Unauthorized , Please register </response> 
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(LoginResponseModel), 200)]
        [ProducesResponseType(typeof(LoginResponseModel), 401)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var userApplication = await FindUserAsync(model?.Identifier ?? "");
            if (userApplication == null)
                return UnauthorizedResponse("Account not found. Please check your credentials and try again.", ["Invalid username or password"]);

            if (!await IsPasswordValidAsync(userApplication, model?.Password ?? ""))
                return UnauthorizedResponse("Password mismatch. Ensure your password is correct and try again.", ["Incorrect password"]);

            var authClaims = await GenerateClaimsAsync(userApplication);
            var encryptedToken = await GetEncryptedToken(userApplication, authClaims);

            await SetUserTokenAsync(userApplication, encryptedToken);

            return Ok(new LoginResponseModel
            {
                Status = ApiResponseStatus.Success.GetDescription(),
                Message = "Welcome back! You're now logged in.",
                Data = new AuthentificationData(_tokenEncryptionService)
                {
                    EncryptedToken = encryptedToken,
                    Expires = DateTime.Now.AddHours(1)
                }
            });
        }
        #endregion

        #region Helpers


        private async Task<UserApplication?> FindUserAsync(string identifier)
        {
            return await _userManager.FindByEmailAsync(identifier) ?? await _userManager.FindByNameAsync(identifier);
        }
        private async Task<bool> IsPasswordValidAsync(UserApplication user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        private async Task<List<Claim>> GenerateClaimsAsync(UserApplication user)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }
        private async Task<string> GetEncryptedToken(UserApplication user, List<Claim> authClaims)
        {
            var AuthenticationToken = await _userManager.GetAuthenticationTokenAsync(user, TokenPurpose.FGPFront, TokenName.WebAuthentication);
            return (AuthenticationToken != null && !_tokenEncryptionService.IsExpired(AuthenticationToken)) ? AuthenticationToken : _tokenEncryptionService.GenerateNewEncrytedToken(authClaims); ;
        }
        private async Task SetUserTokenAsync(UserApplication user, string encryptedToken)
        {
            await _userManager.SetAuthenticationTokenAsync(user, TokenPurpose.FGPFront, TokenName.WebAuthentication, encryptedToken);
        }
        private async Task<bool> SaveUserAppAsync(AppUser user)
        {
            if (user == null)
                return false;

            try
            {
                using var dbContext = _applicationDbContext;
                dbContext.AppUsers.Add(user);
                await dbContext.SaveChangesAsync();

                return true; // SaveChanges succeeded
            }
            catch (Exception ex) when (ex is DbUpdateException || ex is not null)
            {
                throw new Exception(ex.Message);
            }
        }
        private ObjectResult ConflictResponse(string message, string[] errors)
        {
            return StatusCode(StatusCodes.Status409Conflict, CreateRegistrationResponse(ApiResponseStatus.Conflict.GetDescription(), message, errors));
        }
        private ObjectResult UnauthorizedResponse(string message, string[] errors)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, CreateRegistrationResponse(ApiResponseStatus.Unauthorized.GetDescription(), message, errors));
        }
        private ObjectResult ErrorResponse(string message, string[] errors)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, CreateRegistrationResponse(ApiResponseStatus.Error.GetDescription(), message, errors));
        }
        private static RegistrationResponseModel CreateRegistrationResponse(string status, string message, string[] errors)
        {
            return new RegistrationResponseModel
            {
                Status = status,
                Message = message,
                Errors = errors
            };
        }
        private static AppUser CreateUserFromModel(RegisterationModel model)
        {
            return new AppUser()
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };
        }
        private static UserApplication CreateUserApplicationFromModel(RegisterationModel model)
        {
            return new UserApplication()
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
        }
        
        #endregion

    }
}
