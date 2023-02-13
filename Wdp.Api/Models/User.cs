namespace Wdp.Api.Models
{
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNo { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }
    }
}
