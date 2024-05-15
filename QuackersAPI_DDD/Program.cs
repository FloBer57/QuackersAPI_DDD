using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Application.Service;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using QuackersAPI_DDD.Application.Utilitie.UtilitiesServices;
using QuackersAPI_DDD.Domain.Utilitie.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;
using System.Security.Claims;
using System.Text;

namespace QuackersAPI_DDD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ajout des services au conteneur.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Configure Swagger to use the Bearer token for authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21))));
            builder.Services.AddDomainServices();

            // Configuration de l'authentification JWT
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            builder.Services.AddSingleton<ITokenJwtService>(provider =>
                new TokenJwtService(
                    jwtSettings["Key"],
                    jwtSettings["Issuer"],
                    jwtSettings["Audience"]
                ));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RoleClaimType = ClaimTypes.Role
                    };
                });

            var app = builder.Build();

            // Configuration du pipeline HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy"); // Appliquer la politique CORS avant les middlewares d'authentification et d'autorisation

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
