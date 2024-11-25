using PosTech.Entidades;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using PosTech.Exclusao.Consumer.Interfaces;

namespace PosTech.Exclusao.Consumer
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
                var nomeFila = _configuration.GetSection("RabbitMQ3")["NomeFila"] ?? string.Empty;
                var usuario = _configuration.GetSection("RabbitMQ3")["Usuario"] ?? string.Empty;
                var senha = _configuration.GetSection("RabbitMQ3")["Senha"] ?? string.Empty;
                var servidor = _configuration.GetSection("RabbitMQ3")["Servidor"] ?? string.Empty;

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
                    var id = JsonSerializer.Deserialize<int>(message);

                    Console.WriteLine(id.ToString());

                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        var _contatoService = scope.ServiceProvider.GetRequiredService<IContatoServiceExclusaoConsumer>();
                        _contatoService.Deletar(id);
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
