using Microsoft.EntityFrameworkCore;
using Wdp.Api;
using Wdp.Api.Filters;

string CorsPolicy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//允许跨域
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, builder =>
    {
        builder.AllowAnyMethod()
       .AllowAnyHeader()
       .SetIsOriginAllowed(host => true)
       .AllowCredentials();
    });
});

builder.Services.AddControllers(options =>
{
    //注册全局过滤器服务
    options.Filters.Add(typeof(GlobalExceptionsFilter));
    options.Filters.Add(typeof(GlobalAppFilter));
}).AddNewtonsoftJson(options =>
{
    //日期类型格式化
    options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionstring = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<WdpContext>(options =>
{
    options.UseMySql(connectionstring, ServerVersion.AutoDetect("Server=114.115.177.117;Port=3306;Database=wdp;Uid=root;Pwd=zj84787309;"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//app.Use(async (context, next) => {
//    //context.Response.ContentType = "text/plain;charset=utf-8";//防止中文乱码
//    await context.Response.WriteAsync($"第一个中间件输出你好~{Environment.NewLine}");

//    //await context.Response.WriteAsync($"下一个中间件执行前执行===>{Environment.NewLine}");
//    await next.Invoke();
//    //await context.Response.WriteAsync($"下一个中间件执行后执行<==={Environment.NewLine}");
//});

app.MapControllers();




app.Run();
