namespace Wdp.Api
{
    public class ResponseModel
    {
        /// <summary>
        /// 状态 1-成功 2-失败
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        public static ResponseModel Success(object data,string message="操作成功")
        {
            return new ResponseModel
            {
                Status = 1,
                Data = data,
                Message = message
            };
        }

        public static ResponseModel Error(object data,string message="操作失败")
        {
            return new ResponseModel
            {
                Status = 0,
                Data = data,
                Message = message
            };
        }


    }
}
