using PosTech.Contatos.Exclusao.API.Interfaces;
using PosTech.Contatos.Exclusao.API.Services;
using PosTech.Repository.Interfaces;
using PosTech.Repository;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionStringSql"));
    //options.UseLazyLoadingProxies();
    options.EnableSensitiveDataLogging(true); // ADD Antonio José Lima Jr -> 18/05/2024

}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IContatoServiceExclusaoProducer, ContatoServiceExclusaoProducer>();
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
