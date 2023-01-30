using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wdp.Api.Models;

namespace Wdp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly WdpContext db;

        public BillController(WdpContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<List<BillDetail>> GetBillDetailsByUserId(int userId)
        {
            return await db.BillDetails.Where(b => b.UserId == userId).OrderByDescending(b => b.BillDateTime).ToListAsync();
        }

        [HttpPost]
        public async Task<bool> Add(BillDetail bill)
        {
            db.BillDetails.Add(bill);
            int i = await db.SaveChangesAsync();
            return i == 1 ? true : false;
        }

        [HttpDelete]
        public async Task<ResponseModel> Delete(int id)
        {
            var bill = await db.BillDetails.Where((b) => b.Id == id).FirstOrDefaultAsync();

            if (bill == null)
            {
                return ResponseModel.Error("未找到该账单");
            }

            db.BillDetails.Remove(bill);

            int i = await db.SaveChangesAsync();

            return i == 1 ? ResponseModel.Success(null, "删除成功") : ResponseModel.Error("删除失败"); ;
        }


        [HttpGet("test")]
        public void Test()
        {
            Console.WriteLine("开始执行");
            Thread.Sleep(5000);
            Console.WriteLine("执行完成");
        }

    }
}
