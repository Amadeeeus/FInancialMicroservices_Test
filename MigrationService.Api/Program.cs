using Db.Migrations;
using FinanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddDbContext<TokensDbContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("TokensDb")));

builder.Services.AddDbContext<CurrencyDbContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("CurrencyDb")));

builder.Services.AddHostedService<MigrationHostedService>();

var app = builder.Build();

app.Run();