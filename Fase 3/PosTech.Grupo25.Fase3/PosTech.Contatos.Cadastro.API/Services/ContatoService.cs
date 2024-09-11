using PosTech.Contatos.Cadastro.API.Interfaces;
using PosTech.Entidades;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace PosTech.Contatos.Cadastro.API.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IConfiguration _configuration;

        public ContatoService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public void Cadastrar(Contato contato)
        {
            //TODO: ADICIONAR REPOSITORY PARA VERIFICAR REGIÃO
            contato.ValidarEntidade();
            //VerificaRegiaoExiste(contato.RegiaoId);

            var nomeFila = _configuration.GetSection("RabbitMQ")["NomeFila"] ?? string.Empty;
            var usuario = _configuration.GetSection("RabbitMQ")["Usuario"] ?? string.Empty;
            var senha = _configuration.GetSection("RabbitMQ")["Senha"] ?? string.Empty;
            var servidor = _configuration.GetSection("RabbitMQ")["Servidor"] ?? string.Empty;

            var factory = new ConnectionFactory() { HostName = servidor, UserName = usuario, Password = senha };
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: nomeFila,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = JsonSerializer
                    .Serialize(contato);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: nomeFila,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
