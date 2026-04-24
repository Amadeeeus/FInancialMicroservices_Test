using Db.Migrations.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

builder.Services.AddDbContext<UserDbContext>(options 
    => options.UseNpgSql(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddDbContext<UserDbContext>(options 
    => options.UseNpgSql(builder.Configuration.GetConnectionString("FinancionalDb")));

builder.Services.AddHostedService<MigrationHostedService>();

var app = builder.Build();

app.Run();