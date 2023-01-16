namespace Wdp.Api.Models
{
    public class FavWebsite
    {
        public int Id { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 网站分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 网站图标icon
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
