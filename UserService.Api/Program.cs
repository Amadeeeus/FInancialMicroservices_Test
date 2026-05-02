using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using UserService.Infrastructure;
using UserServiceApplication;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите токен так: Bearer {token}"
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
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();

builder.Services.AddHealthChecks()
    .AddNpgSql(configuration.GetConnectionString("UserDb")!, name: "UserDbHealthCheck")
    .AddNpgSql(configuration.GetConnectionString("TokensDb")!,  name: "TokensDbHealthCheck");

builder.Services.AddApplication(configuration).AddInfrastructure(configuration);

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var (status, message) = exception switch
        {
            UnauthorizedAccessException => (401, "Unauthorized"),
            KeyNotFoundException => (404, "Not found"),
            ArgumentException e => (400, e.Message),
            _ => (500, "Internal server error")
        };
        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new { error = message });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapControllers();
app.Run();