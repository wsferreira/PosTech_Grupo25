using Microsoft.EntityFrameworkCore;
using PosTech.Contatos.API.Interfaces;
using PosTech.Repository;
using PosTech.Contatos.API.Services;
using Prometheus;
using System.Text.Json.Serialization;
using PosTech.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ADD Antonio José Lima Jr -> 18/05/2024
// adicionado para resolver o problema de circle de referencias 

//builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionStringSql"));
    //options.UseLazyLoadingProxies();
    options.EnableSensitiveDataLogging(true); // ADD Antonio José Lima Jr -> 18/05/2024

}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseHttpMetrics();

app.UseAuthorization();

app.MapMetrics();
app.MapControllers();

app.Run();
