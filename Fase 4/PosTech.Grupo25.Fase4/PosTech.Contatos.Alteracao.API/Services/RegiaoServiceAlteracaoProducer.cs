using PosTech.Contatos.Alteracao.API.Interfaces;
using PosTech.Entidades;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using PosTech.Repository.Interfaces;
using System.Configuration;

namespace PosTech.Contatos.Alteracao.API.Services
{
    public class RegiaoServiceAlteracaoProducer : IRegiaoServiceAlteracaoProducer
    {
        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoServiceAlteracaoProducer(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public Regiao ObterPorId(int id)
        {
            return _regiaoRepository.ObterPorId(id);
        }

        public IEnumerable<Regiao> ObterTodos()
        {
            return _regiaoRepository.ObterTodos();
        }
    }
}
