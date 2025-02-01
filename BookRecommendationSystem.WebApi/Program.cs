using BookRecommendationSystem.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder
    .AddData();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
