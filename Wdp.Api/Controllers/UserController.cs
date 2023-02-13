using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wdp.Api.Config;
using Wdp.Api.Models;

namespace Wdp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WdpContext db;
        private readonly ILogger<UserController> _logger;
        private readonly IOptionsSnapshot<JWTOptions> jwtOptions;

        public UserController(WdpContext _db, ILogger<UserController> logger, IOptionsSnapshot<JWTOptions> jwtOpt)
        {
            db = _db;
            _logger = logger;
            jwtOptions = jwtOpt;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            db.Users.Add(user);
            int i = await db.SaveChangesAsync();
            return Ok(i);
        }
        [HttpGet]
        public async Task<ResponseModel> Test(string url)
        {
            _logger.LogInformation("网址为:" + url);
            string iconUrl = await Utils.GetWebsiteIconUrlAsync(url);
            _logger.LogError("网址为:" + url);
            _logger.LogWarning("网址为:" + url);
            
            return ResponseModel.Success(iconUrl);
        }

        [HttpPost("login")]
        public async Task<ResponseModel> Login(string name)
        {
            name = name.ToUpper();
            if(name  == "ZL" || name == "TQ" || name == "LFF")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, name));

                var jwtOtp = jwtOptions.Value;
                DateTime expires = DateTime.Now.AddSeconds(jwtOtp.ExpireSeconds);
                byte[] secBytes = Encoding.UTF8.GetBytes(jwtOtp.SigningKey);
                var secKey = new SymmetricSecurityKey(secBytes);
                var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);
                var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expires, signingCredentials: credentials);
                string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                return ResponseModel.Success(jwt);
            }
            return ResponseModel.Error("非法用户");
        }
    }
}
