using Common.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace Common.Initializer;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureDbConfiguration(this WebApplicationBuilder builder)
    {
#pragma warning disable ASP0013 // Suggest switching from using Configure methods to WebApplicationBuilder.Configuration
        builder.Host.ConfigureAppConfiguration((hostCtx, configBuilder) =>
        {
            string? connStr = builder.Configuration.GetValue<string>("DefaultDB:ConnStr");
            configBuilder.AddDbConfiguration(() => new NpgsqlConnection(connStr), reloadOnChange: true, tableName:"Configurations", reloadInterval: TimeSpan.FromSeconds(5));
        });
#pragma warning restore ASP0013 // Suggest switching from using Configure methods to WebApplicationBuilder.Configuration
    }

    public static void ConfigureExtraServices(this WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        IConfiguration configuration = builder.Configuration;
        
        services.AddAuthorization();
        services.AddAuthentication();
        JWTOptions? jwtOpt = configuration.GetSection("JWT").Get<JWTOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOpt.Issuer,
                ValidAudience = jwtOpt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.Key))
            };
        });
        services.Configure<SwaggerGenOptions>(c =>
        {
            c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
            {
                Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Authorization"
            }); ;
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Authorization"
                        },
                        Scheme = "oauth2",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
        services.AddLogging();
        services.Configure<JWTOptions>(configuration.GetSection("JWT"));
    }
}
