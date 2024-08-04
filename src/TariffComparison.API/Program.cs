using TariffComparison.API.Extensions;
using TariffComparison.Application.Interfaces;
using TariffComparison.Domain.Helpers;
using TariffComparison.Infrastructure;
using TariffComparison.Infrastructure.Options;
using TariffComparison.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TariffCalculatorGenerator>();
builder.Services.AddSingleton<TariffProviderContext>();
builder.Services.AddScoped<ITariffRepository, TariffRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.Configure<TariffProviderDatabaseSettings>(builder.Configuration.GetSection("TariffProviderDatabase"));

builder.Services.AddMinimalEndpoints();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;

    var context = scopedProvider.GetRequiredService<TariffProviderContext>();

    var seeder = new Seeder(context);
    seeder.Seed();
}

app.RegisterMinimalEndpoints();

app.Run();

public partial class Program
{ }