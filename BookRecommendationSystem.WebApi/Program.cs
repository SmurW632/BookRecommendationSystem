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

builder
    .AddSwagger()
    .AddData()
    .AddAutoMapper()
    .AddRepositories()
    .AddServices();


var app = builder.Build();

app.UseHttpLogging();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();


app.Run();
