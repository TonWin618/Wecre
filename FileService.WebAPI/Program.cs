using FileService.Domain;
using FileService.Infrastructure;
using FileService.Infrastructure.Services;
using FileService.WebAPI.Controllers;
using Microsoft.EntityFrameworkCore;

ConfigurationBuilder configurationBuilder = new();
configurationBuilder.AddUserSecrets<Program>();
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<FileDomainService>();
builder.Services.AddScoped<IStorageClient, TencentStorageClient>();
builder.Services.AddScoped<IStorageClient, SMBStorageClient>();
builder.Services.Configure<SMBStorageOptions>(builder.Configuration.GetSection("SMBStorageClient"))
    .Configure<TencentStorageOptions>(builder.Configuration.GetSection("TencentStorageClient"));

builder.Services.AddDbContext<FileDbContext>(opt =>
{
    string connStr = Environment.GetEnvironmentVariable("DefaultDB:ConnStr");
    opt.UseNpgsql(connStr);
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
