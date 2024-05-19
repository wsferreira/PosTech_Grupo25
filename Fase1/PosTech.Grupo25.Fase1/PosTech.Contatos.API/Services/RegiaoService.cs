using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;
using PosTech.Contatos.API.Repository;

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

        public IList<Regiao> ObterTodos()
        {
            return _regiaoRepository.ObterTodos();
        }
    }
}
