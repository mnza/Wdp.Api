using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if(string.IsNullOrWhiteSpace(fav.Url)) return ResponseModel.Error("网址不能为空");

            fav.Icon = await Utils.GetWebsiteIconUrlAsync(fav.Url);

            db.FavWebsites.Add(fav);

            int i = await db.SaveChangesAsync();

            if (i == 1)
            {
                return ResponseModel.Success(fav,"保存成功");
            }

            return ResponseModel.Error("保存失败");

        }

        [HttpGet]
        public async Task<List<FavWebsite>> GetWebsiteByUserId(int userId)
        {
            return await db.FavWebsites.Where(b => b.UserId == userId).ToListAsync();
        }
    }
}
