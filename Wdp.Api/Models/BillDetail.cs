namespace Wdp.Api.Models
{
    public class BillDetail
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int UserId { get; set; }
        public DateTime BillDateTime { get; set; }
        public DateTime OperatorTime { get; set; }
        public string Remark { get; set; }

    }
}
