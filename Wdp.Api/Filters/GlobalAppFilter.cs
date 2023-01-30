using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Net;

namespace Wdp.Api.Filters
{
    public class GlobalAppFilter : Attribute, IActionFilter
    {
        /// <summary>
        /// 请求时长计时开始
        /// </summary>
        private readonly Stopwatch watch = new Stopwatch();

        public void OnActionExecuted(ActionExecutedContext context)
        {
            watch.Stop();
            //根据实际需求进行具体实现
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult.Value == null)
                {
                    context.Result = new ObjectResult(new ResponseModel
                    {
                        Status = 0,
                        Data = null,
                        Message = "未找到资源"
                    });
                }
                else
                {
                    if (objectResult.Value is ResponseModel)
                    {
                        var result = (ResponseModel)objectResult.Value;
                        context.Result = new ObjectResult(result);
                        return;
                    }
                    context.Result = new ObjectResult(new ResponseModel { Status = ResponseStatus.Success, Message = "请求成功", Data = objectResult.Value });
                    //判读是否返回的是元组
                    //返回数据并且返回总行数 public async Task<（List<object>,int）> Paging(PagingBaseRequest parameter) { retuen ( new List<object>{ },1000) }
                    if (objectResult.DeclaredType != null && objectResult.DeclaredType.Name == "ValueTuple`2")
                    {
                        dynamic value = objectResult.Value;
                        if (value.Item1 != null)
                        {
                            if (value.Item1 is int)
                            {
                                //返回元组格式（int,List<object>）
                                context.Result = new ObjectResult(new ResponseModel { Status = ResponseStatus.Success, Message = "请求成功", Data = new { Count = value.Item1, Data = value.Item2 } });
                            }
                            else
                                //返回元组格式（List<object>,int）
                                context.Result = new ObjectResult(new ResponseModel { Status = ResponseStatus.Success, Message = "请求成功", Data = new { Count = value.Item2, Data = value.Item1 } });
                        }
                    }

                }
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(new ResponseModel { Status = ResponseStatus.Success, Message = "请求成功", Data = null });
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new ObjectResult(new ResponseModel { Status = ResponseStatus.Success, Message = "", Data = (context.Result as ContentResult).Content });
            }
            else if (context.Result is StatusCodeResult)
            {
                context.Result = new ObjectResult(new { HttpStatus = (context.Result as StatusCodeResult).StatusCode, TimeOut = watch.ElapsedMilliseconds, Message = "" });
            }
            else if (context.Result is Exception)
            {
                var result = context.Result as Exception;
                context.Result = new ObjectResult(new { HttpStatus = HttpStatusCode.BadRequest, TimeOut = watch.ElapsedMilliseconds, Message = result.Message });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            watch.Start();
        }
    }
}
