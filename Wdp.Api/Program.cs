using Microsoft.EntityFrameworkCore;
using Wdp.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
