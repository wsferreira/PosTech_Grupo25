using Microsoft.EntityFrameworkCore;
using PosTech.Cadastro.Consumer;
using PosTech.Repository.Interfaces;
using PosTech.Repository;
using PosTech.Cadastro.Consumer.Services;
using PosTech.Cadastro.Consumer.Interfaces;

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

builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
