using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;
using Wdp.Api;
using Wdp.Api.Config;
using Wdp.Api.Filters;
using Wdp.Api.Middlewares;
using Wdp.Api.Models;

var builder = WebApplication.CreateBuilder(args);

string CorsPolicy = "CorsPolicy";

#region ������־

//��־���·��
string LogFilePath(string LogEvent) => $@"{AppContext.BaseDirectory}Logs\{LogEvent}\log.log";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()//��С��־�������Ϊdebug
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//��������Microsoft��ͷ�Ļ�����С�������information
    .Enrich.FromLogContext()//ʹ��Serilog.Context.LogContext�е����Էḻ��־�¼���
                            //.WriteTo.Console(new RenderedCompactJsonFormatter()) //��json��ʽ���������̨���������ر��ң�2022-04-11 zl
    .WriteTo.Console()
    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.File(LogFilePath("Debug"), rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.File(LogFilePath("Information"), rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.File(LogFilePath("Warning"), rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.File(LogFilePath("Error"), rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.File(LogFilePath("Fatal"), rollingInterval: RollingInterval.Day))
    .CreateLogger();

builder.Host.UseSerilog();
#endregion

#region ��������
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

#endregion

#region ���ù�����
builder.Services.AddControllers(options =>
{
    //ע��ȫ�ֹ���������
    options.Filters.Add(typeof(JWTValidationFilter));
    options.Filters.Add(typeof(GlobalExceptionsFilter));
    options.Filters.Add(typeof(GlobalAppFilter));
}).AddNewtonsoftJson(options =>
{
    //�������͸�ʽ��
    options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
});
#endregion

builder.Services.AddEndpointsApiExplorer();

#region ����Swagger
builder.Services.AddSwaggerGen(c =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header.\r\nExample:'Bearer 12345'",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization"
        },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    };
    c.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    c.AddSecurityRequirement(requirement);
});

#endregion

#region �������ݿ�������
var connectionstring = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<WdpContext>(options =>
{
    options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring));
    options.LogTo(Log.Information);//sql�������־
});

#endregion

#region ����JWT
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    var jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTOptions>();
    byte[] keyBytes = Encoding.UTF8.GetBytes(jwtOpt.SigningKey);
    var secKey = new SymmetricSecurityKey(keyBytes);
    x.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = secKey
    };
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if(!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/Hubs/ChatRoomHub")))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

#endregion

#region ����SignalR
builder.Services.AddSignalR().AddStackExchangeRedis("114.115.177.117",options => {
    options.Configuration.Password = "zj84787309";
    options.Configuration.ChannelPrefix = "ChatRoom";
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(CorsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.UseTestMiddleware();

app.MapHub<ChatRoomHub>("/Hubs/ChatRoomHub");

app.MapControllers();


app.Run();
