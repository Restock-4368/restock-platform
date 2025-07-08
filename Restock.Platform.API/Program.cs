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
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.OpenApi.Models;
using Restock.Platform.API.IAM.Application.Internal.CommandServices;
using Restock.Platform.API.IAM.Application.Internal.OutboundServices;
using Restock.Platform.API.IAM.Application.Internal.OutboundServices.ACL;
using Restock.Platform.API.IAM.Application.Internal.QueryServices;
using Restock.Platform.API.IAM.Domain.Model.Commands;
using Restock.Platform.API.IAM.Domain.Repositories;
using Restock.Platform.API.IAM.Domain.Services;
using Restock.Platform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Restock.Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Restock.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using Restock.Platform.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Restock.Platform.API.IAM.Infrastructure.Tokens.JWT.Services;
using Restock.Platform.API.IAM.Interfaces.ACL;
using Restock.Platform.API.IAM.Interfaces.ACL.Services;
using Restock.Platform.API.Profiles.Application.ACL;
using Restock.Platform.API.Profiles.Application.Internal.CommandServices;
using Restock.Platform.API.Profiles.Application.Internal.QueryServices;
using Restock.Platform.API.Profiles.Domain.Repositories;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using Restock.Platform.API.Profiles.Infrastructure.Persistence.Seeders;
using Restock.Platform.API.Profiles.Interfaces.ACL;
using Restock.Platform.API.Resource.Application.Internal.CommandServices;
using Restock.Platform.API.Resource.Application.Internal.QueryServices;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;
using Restock.Platform.API.Resource.Infrastructure.Persistence.EFC.Repositories;
using Restock.Platform.API.Resource.Infrastructure.Persistence.Seeders;
using Restock.Platform.API.Shared.Domain.Exceptions;
using Restock.Platform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(int.Parse(port));
});

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
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Configuration for Entity Framework Core
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") 
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());
else if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors());

// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen(options => {
    options.EnableAnnotations();
});
// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen(options =>
{
    // General API Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UI-Topic.RestockPlatform.API",
        Version = "v1",
        Description = "UI-Topic RestockPlatform Platform API", 
        Contact = new OpenApiContact
        {
            Name = "UI-Topic Studios",
            Email = "uitopic@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        },
    });
    
    // Enable Annotations for Swagger
    options.EnableAnnotations();
    
    // Add Bearer Authentication for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    // Add Security Requirement for Swagger
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
 
// Planning Bounded Context
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeCommandService, RecipeCommandService>();
builder.Services.AddScoped<IRecipeQueryService, RecipeQueryService>();

//Resource Bounded Context
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();

builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<IBatchCommandService, BatchCommandService>();
builder.Services.AddScoped<IBatchQueryService, BatchQueryService>();

builder.Services.AddScoped<ISupplyRepository, SupplyRepository>(); 
builder.Services.AddScoped<ISupplyQueryService, SupplyQueryService>();

builder.Services.AddScoped<ICustomSupplyRepository, CustomSupplyRepository>(); 
builder.Services.AddScoped<ICustomSupplyCommandService, CustomSupplyCommandService>(); 
builder.Services.AddScoped<ICustomSupplyQueryService, CustomSupplyQueryService>(); 
 
//Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IBusinessCommandService, BusinessCommandService>();
builder.Services.AddScoped<IBusinessQueryService, BusinessQueryService>();

builder.Services.AddScoped<IBusinessCategoryRepository, BusinessCategoryRepository>(); 
builder.Services.AddScoped<IBusinessCategoryQueryService, BusinessCategoryQueryService>();


// IAM Bounded Context

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddScoped<ExternalProfilesService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>(); 
builder.Services.AddScoped<IRoleQueryService, RoleQueryService>();
builder.Services.AddScoped<IRoleCommandService, RoleCommandService>();

// Add Mediator for CQRS
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
        //options.AddDefaultBehaviors();
    });

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
        
    await SupplySeeder.SeedAsync(services);
    await BusinessCategorySeeder.SeedAsync(services);
    
    var roleSeeder = scope.ServiceProvider.GetRequiredService<IRoleCommandService>();
    await roleSeeder.Handle(new SeedRolesCommand());
}


// Use Swagger for API documentation if in development mode
// if (app.Environment.IsDevelopment())
// {
     app.UseSwagger();
     app.UseSwaggerUI();
// }

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (BusinessRuleException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { error = "Internal Server Error" });
    }
});

// app.UseHttpsRedirection();

app.UseRequestLocalization();

app.UseAuthentication(); 
app.UseAuthorization();

// app.UseRequestAuthorization();

app.MapControllers();

app.Run();