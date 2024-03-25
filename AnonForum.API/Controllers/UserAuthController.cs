using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnonForum.API.BLL.DTOs.User;
using AnonForum.API.BLL.Interfaces;
using AnonForum.API.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnonForum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserBLL _userBLL;
        private readonly AppSettings _appSettings;
        private readonly IValidator<CreateUserDTO> _validatorCreateUserDto;
        private readonly ILogger<UserAuthController> _logger;

        public UserAuthController(IUserBLL userBLL, IOptions<AppSettings> appSettings, IValidator<CreateUserDTO> validatorCreateUserDto, ILogger<UserAuthController> logger)
        {
            _userBLL = userBLL;
            _appSettings = appSettings.Value;
            _validatorCreateUserDto = validatorCreateUserDto;
            _logger = logger;
        }
        // GET: api/<UserAuthController>
        [Authorize]
        //[Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAsync()
        {
            var users = await _userBLL.GetUsersWithRoles();
            return users;
        }

        // GET api/<UserAuthController>/5
        //[Authorize]
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("{id}")]
        public async Task<UserDTO> Get(int id)
        {
            var user = await _userBLL.GetUserbyIDWithRoles(id);
            return user;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            try
            {
                var user = await _userBLL.UserLogin(userLogin);
                if (user == null)
                {
                    return Unauthorized();
                }
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Username));
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                //_logger.LogInformation($"---------------------------> {_appSettings.Secret}");
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(30),
                    SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                //_logger.LogInformation($"---------------------------> Token generated for {tokenHandler.WriteToken(token)}");
                var tokenDto = new UserWithToken
                {
                    Username = user.Username,
                    Token = tokenHandler.WriteToken(token)
                };
                return Ok(tokenDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException.Message}");
            }
        }

        // POST api/<UserAuthController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO createUser)
        {
            try
            {
                await _userBLL.AddNewUser(createUser);
                return CreatedAtAction("Get", createUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UserAuthController>/5
        //[Authorize]
        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userBLL.GetUserbyID(id);
                if (user == null)
                {
                    return NotFound();
                }
                await _userBLL.DeleteUser(user.Username);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("Claims")]
        public IActionResult GetClaims()
        {
            var claims = new List<string>();
            foreach (var claim in User.Claims)
            {
                claims.Add($"{claim.Type}: {claim.Value}");
            }
            return Ok(claims);
        }
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(string username, string newPassword)
        {
            _userBLL.ChangePassword(username, newPassword);
            return Ok();
        }
    }
}
