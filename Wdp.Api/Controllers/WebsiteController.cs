using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wdp.Api.Models;

namespace Wdp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly WdpContext db;

        public WebsiteController(WdpContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<ResponseModel> Add(FavWebsite fav)
        {
            db.FavWebsites.Add(fav);

            int i = await db.SaveChangesAsync();
            if (i == 1)
            {
                return ResponseModel.Success("");
            }

            return ResponseModel.Error("保存失败");

        }
    }
}
