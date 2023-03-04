using Common.Initializer;
using FileService.Domain;
using FileService.Infrastructure;
using FileService.Infrastructure.Services;
using FileService.WebAPI.Controllers;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FileDomainService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IStorageClient, TencentStorageClient>();
builder.Services.AddScoped<IStorageClient, SMBStorageClient>();

builder.Services.Configure<SMBStorageOptions>(builder.Configuration.GetSection("SMBStorageClient"))
    .Configure<TencentStorageOptions>(builder.Configuration.GetSection("TencentStorageClient"));

builder.Services.AddDbContext<FileDbContext>(opt =>
{
    string? connStr = Environment.GetEnvironmentVariable("DefaultDB:ConnStr");
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
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
