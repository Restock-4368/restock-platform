using System.Text.Json;
using Restock.Platform.API.Shared.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Planning.Application.Internal.CommandServices;
using Restock.Platform.API.Planning.Application.Internal.QueryServices;
using Restock.Platform.API.Planning.Domain.Repositories;
using Restock.Platform.API.Planning.Domain.Services;
using Restock.Platform.API.Planning.Infrastructure.Persistence.EFC.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add ASP.NET Core MVC with Kebab Case Route Naming Convention
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(
    options => options.Conventions.Add(new KebabCaseRouteNamingConvention())
    )
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    });

builder.Services.AddEndpointsApiExplorer();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Configuration for Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen(options => {
    options.EnableAnnotations();
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Planning Bounded Context
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeCommandService, RecipeCommandService>();
builder.Services.AddScoped<IRecipeQueryService, RecipeQueryService>();

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>(); 
    context.Database.EnsureCreated();
}


// Use Swagger for API documentation if in development mode
// if (app.Environment.IsDevelopment())
// {
     app.UseSwagger();
     app.UseSwaggerUI();
// }

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();