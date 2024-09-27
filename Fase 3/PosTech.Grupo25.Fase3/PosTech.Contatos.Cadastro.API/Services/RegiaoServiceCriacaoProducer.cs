using PosTech.Contatos.Cadastro.API.Interfaces;
using PosTech.Entidades;
using PosTech.Repository.Interfaces;

namespace PosTech.Contatos.Cadastro.API.Services
{
    public class RegiaoServiceCriacaoProducer : IRegiaoServiceCriacaoProducer
    {
        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoServiceCriacaoProducer(IRegiaoRepository regiaoRepository)
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
