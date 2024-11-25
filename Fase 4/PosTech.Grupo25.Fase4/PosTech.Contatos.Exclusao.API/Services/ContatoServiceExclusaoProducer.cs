using PosTech.Contatos.Exclusao.API.Interfaces;
using PosTech.Entidades;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using PosTech.Repository.Interfaces;
using PosTech.Repository;

namespace PosTech.Contatos.Exclusao.API.Services
{
    public class ContatoServiceExclusaoProducer : IContatoServiceExclusaoProducer
    {
        private readonly IConfiguration _configuration;
        private readonly IContatoRepository _contatoRepository;
 

        public ContatoServiceExclusaoProducer(IConfiguration configuration, IContatoRepository contatoRepository)
        {
            _configuration = configuration;
            _contatoRepository = contatoRepository;

        }
        public void Deletar(int id)
        {
            VerificaContatoExiste(id);

            var nomeFila = _configuration.GetSection("RabbitMQ3")["NomeFila"] ?? string.Empty;
            var usuario = _configuration.GetSection("RabbitMQ3")["Usuario"] ?? string.Empty;
            var senha = _configuration.GetSection("RabbitMQ3")["Senha"] ?? string.Empty;
            var servidor = _configuration.GetSection("RabbitMQ3")["Servidor"] ?? string.Empty;

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
                    .Serialize(id);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: nomeFila,
                    basicProperties: null,
                    body: body);
            }
        }

        private Contato VerificaContatoExiste(int id)
        {
            var cont = _contatoRepository.ObterPorId(id);
            if (cont == null)
            {
                throw new DomainException("Contato não encontrado.");
            }
            return cont;
        }

    }
}
