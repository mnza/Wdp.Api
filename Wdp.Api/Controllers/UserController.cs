using Microsoft.AspNetCore.Mvc;
using Wdp.Api.Models;

namespace Wdp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WdpContext db;

        public UserController(WdpContext _db)
        {
            db = _db;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users user)
        {
            db.Users.Add(user);
            int i = await db.SaveChangesAsync();
            return Ok(i);
        }
        [HttpGet]
        public async Task<ResponseModel> Test(string url)
        {
            string iconUrl = await Utils.GetWebsiteIconUrlAsync(url);
            return ResponseModel.Success(iconUrl);
        }
    }
}
