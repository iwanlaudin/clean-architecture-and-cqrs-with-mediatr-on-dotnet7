using ItechCleanArst.Persistence;
using ItechCleanArst.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ItechCleanArst.Api.OptionsSetup;
using ItechCleanArst.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration, "DefaultConnection");
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configure =>
{
    configure.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "JWT"
        },
        In = ParameterLocation.Header
    };

    var requirement = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };

    configure.AddSecurityRequirement(requirement);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
