using Microsoft.EntityFrameworkCore;
using Wdp.Api;
using Wdp.Api.Filters;

string CorsPolicy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//�������
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
    //ע��ȫ�ֹ���������
    options.Filters.Add(typeof(GlobalExceptionsFilter));
    options.Filters.Add(typeof(GlobalAppFilter));
}).AddNewtonsoftJson(options =>
{
    //�������͸�ʽ��
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
//    //context.Response.ContentType = "text/plain;charset=utf-8";//��ֹ��������
//    await context.Response.WriteAsync($"��һ���м��������~{Environment.NewLine}");

//    //await context.Response.WriteAsync($"��һ���м��ִ��ǰִ��===>{Environment.NewLine}");
//    await next.Invoke();
//    //await context.Response.WriteAsync($"��һ���м��ִ�к�ִ��<==={Environment.NewLine}");
//});

app.MapControllers();




app.Run();
