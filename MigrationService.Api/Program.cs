using Db.Migrations;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

builder.Services.AddDbContext<UserDbContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddDbContext<UserDbContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("FinancionalDb")));

builder.Services.AddHostedService<MigrationHostedService>();

var app = builder.Build();

app.Run();