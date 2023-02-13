namespace Wdp.Api.Models
{
    public class BillMaster
    {
        /// <summary>
        /// 账单ID 主键
        /// </summary>
        public Guid BillId { get; set; }
        /// <summary>
        /// 账单名称
        /// </summary>
        public string BillName { get; set; }
        /// <summary>
        /// 账单日期
        /// </summary>
        public DateTime BillDateTime { get; set; }
        /// <summary>
        /// 账单金额
        /// </summary>
        public double BillMoney { get; set; }
        /// <summary>
        /// 账单备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 账单项目明细
        /// </summary>
        public List<BillDetail> BillDetails { get; set; }
        /// <summary>
        /// 记账人
        /// </summary>
        public User User { get; set; }
    }
}
