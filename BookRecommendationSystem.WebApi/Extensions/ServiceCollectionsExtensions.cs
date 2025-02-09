using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.Services;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Enums;
using BookRecommendationSystem.Domain.Options;
using BookRecommendationSystem.Domain.Repositories;
using BookRecommendationSystem.Infrastructure;
using BookRecommendationSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookRecommendationSystem.WebApi.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }

        public static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Application.Mapping.MappingProfile).Assembly);

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
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole(RoleConsts.Admin));
                options.AddPolicy("User", policy => policy.RequireRole(RoleConsts.User));
            });
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddDefaultIdentity<UserEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddUserManager<UserManager<UserEntity>>()
                .AddUserStore<UserStore<UserEntity, IdentityRoleEntity, AppDbContext, int>>();

            return builder;
        }

        public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Authentication"));

            return builder;
        }

        public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            return builder;
        }

        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            return builder;
        }
    }
}
