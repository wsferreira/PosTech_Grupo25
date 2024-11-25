using PosTech.Contatos.API.Interfaces;
using PosTech.Entidades;
using PosTech.Repository.Interfaces;

namespace PosTech.Contatos.API.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoService(IRegiaoRepository regiaoRepository)
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
