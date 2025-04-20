using BookRecommendationSystem.Infrastructure;
using BookRecommendationSystem.WebApi.Extensions;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpLogging(opt =>
{
    opt.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.RequestHeaders |
                        HttpLoggingFields.Duration | HttpLoggingFields.RequestPath | HttpLoggingFields.ResponseBody |
                        HttpLoggingFields.ResponseHeaders;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection"));

builder
    .AddApplicationServices()
    .AddSwagger()
    .AddBearerAuthentication()
    .AddIdentityConfiguration()
    .AddAutoMapper()
    .ConfigureOptions();


var app = builder.Build();

app.UseHttpLogging();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAllOrigins");
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

app.Run();
