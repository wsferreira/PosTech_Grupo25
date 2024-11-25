using PosTech.Contatos.Alteracao.API.Interfaces;
using PosTech.Entidades;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using PosTech.Repository.Interfaces;
using System.Configuration;

namespace PosTech.Contatos.Alteracao.API.Services
{
    public class ContatoServiceAlteracaoProducer : IContatoServiceAlteracaoProducer
    {
        private readonly IConfiguration _configuration;
        private readonly IContatoRepository _contatoRepository;
        private readonly IRegiaoRepository _regiaoRepository;

        public ContatoServiceAlteracaoProducer(IConfiguration configuration, IContatoRepository contatoRepository, IRegiaoRepository regiaoRepository)
        {
            _configuration = configuration;
            _contatoRepository = contatoRepository;
            _regiaoRepository = regiaoRepository;
        }

        public void Alterar(Contato contato)
        {
            contato.ValidarEntidade();
            VerificaContatoExiste(contato.Id);
            VerificaRegiaoExiste(contato.RegiaoId);

            var nomeFila = _configuration.GetSection("RabbitMQ2")["NomeFila"] ?? string.Empty;
            var usuario = _configuration.GetSection("RabbitMQ2")["Usuario"] ?? string.Empty;
            var senha = _configuration.GetSection("RabbitMQ2")["Senha"] ?? string.Empty;
            var servidor = _configuration.GetSection("RabbitMQ2")["Servidor"] ?? string.Empty;

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
        private Contato VerificaContatoExiste(int id)
        {
            var cont = _contatoRepository.ObterPorId(id);
            if (cont == null)
            {
                throw new DomainException("Contato não encontrado.");
            }
            return cont;
        }
        protected void VerificaRegiaoExiste(int id)
        {
            var cont = _regiaoRepository.ObterPorId(id);
            if (cont == null)
            {
                throw new DomainException("DDD não encontrado.");
            }
        }
    }
}
