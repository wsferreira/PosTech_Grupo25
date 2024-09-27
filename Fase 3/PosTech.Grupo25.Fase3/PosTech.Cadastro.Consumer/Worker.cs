using Microsoft.Extensions.Configuration;
using PosTech.Cadastro.Consumer.Interfaces;
using PosTech.Entidades;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PosTech.Cadastro.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        //private readonly IContatoService _contatoService;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var nomeFila = _configuration.GetSection("RabbitMQ")["NomeFila"] ?? string.Empty;
                var usuario = _configuration.GetSection("RabbitMQ")["Usuario"] ?? string.Empty;
                var senha = _configuration.GetSection("RabbitMQ")["Senha"] ?? string.Empty;
                var servidor = _configuration.GetSection("RabbitMQ")["Servidor"] ?? string.Empty;

                var factory = new ConnectionFactory() { HostName = servidor, UserName = usuario, Password = senha };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: nomeFila,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var contato = JsonSerializer.Deserialize<Contato>(message);

                    Console.WriteLine(contato?.ToString());

                    if (contato != null)
                    {
                        using(IServiceScope scope = _serviceProvider.CreateScope())
                        {
                            var _contatoService = scope.ServiceProvider.GetRequiredService<IContatoServiceCriacaoConsumer>();
                            _contatoService.Cadastrar(contato);
                        }
                        
                    }
                };

                channel.BasicConsume(
                    queue: nomeFila,
                    autoAck: true,
                    consumer: consumer);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
