using Microsoft.EntityFrameworkCore;
using PosTech.Exclusao.Consumer;
using PosTech.Repository.Interfaces;
using PosTech.Repository;
using PosTech.Exclusao.Consumer.Services;
using PosTech.Exclusao.Consumer.Interfaces;

var builder = Host.CreateApplicationBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionStringSql"));
    //options.UseLazyLoadingProxies();
    options.EnableSensitiveDataLogging(true); // ADD Antonio José Lima Jr -> 18/05/2024

}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IContatoServiceExclusaoConsumer, ContatoServiceExclusaoConsumer>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
