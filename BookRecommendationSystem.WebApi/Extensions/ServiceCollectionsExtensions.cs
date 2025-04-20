using BookRecommendationSystem.Application.Moduls.Authors.Services;
using BookRecommendationSystem.Application.Moduls.Books.Services;
using BookRecommendationSystem.Application.Moduls.Customers.Services;
using BookRecommendationSystem.Application.Moduls.Genres.Services;
using BookRecommendationSystem.Application.Shared.Mapping;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Shared.Models;
using BookRecommendationSystem.Domain.Shared.Options;
using BookRecommendationSystem.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookRecommendationSystem.WebApi.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return builder;
        }

        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "BookService API",
                    Version = "v1",
                });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Пожалуйста введите верный токен",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return builder;
        }

        public static WebApplicationBuilder AddBearerAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.UseSecurityTokenValidators = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                            builder.Configuration["Authentication:TokenPrivateKey"]!)),
                        ValidIssuer = "test",
                        ValidAudience = "test",
                        //ValidateIssuer = true,
                        //ValidateAudience = true,
                        //ValidateLifetime = true,
                        //ValidateIssuerSigningKey = true
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false
                    };
                });

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("Admin", policy => policy.RequireRole(RoleConsts.Admin))
                .AddPolicy("User", policy => policy.RequireRole(RoleConsts.User));

            return builder;
        }

        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultIdentity<IdentityUserEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRoleEntity>()
            .AddEntityFrameworkStores<AppDbContext>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Authentication"));

            return builder;
        }

        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            return builder;
        }
    }
}
