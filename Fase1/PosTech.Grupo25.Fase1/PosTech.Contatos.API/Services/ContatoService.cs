using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IRegiaoRepository _regiaoRepository;

        public ContatoService(IContatoRepository contatoRepository, IRegiaoRepository regiaoRepository)
        {
            _contatoRepository = contatoRepository;
            _regiaoRepository = regiaoRepository;
        }

        public void Alterar(Contato contato)
        {
            VerificaContatoExiste(contato.Id);

            VerificaRegiaoExiste(contato.RegiaoId);

            _contatoRepository.Alterar(contato);
        }

        public void Cadastrar(Contato contato)
        {
            VerificaRegiaoExiste(contato.RegiaoId);

            _contatoRepository.Cadastrar(contato);
           
        }

        public void Deletar(int id)
        {
            VerificaContatoExiste(id);

            _contatoRepository.Deletar(id);
        }

        public IList<Contato> ObterContatosPorRegiao(int regiaoDDD)
        {
            return _contatoRepository.ObterContatosPorRegiao(regiaoDDD);
        }

        public Contato ObterPorId(int id)
        {
            return _contatoRepository.ObterPorId(id);
        }

        public IList<Contato> ObterTodos()
        {
            return _contatoRepository.ObterTodos();
        }

        private void VerificaContatoExiste(int id)
        {
            var cont = _contatoRepository.ObterPorId(id);
            if (cont == null)
            {
                throw new DomainException("Contato não encontrado.");
            }
        }

        private void VerificaRegiaoExiste(int id)
        {
            var cont = _regiaoRepository.ObterPorId(id);
            if (cont == null)
            {
                throw new DomainException("DDD não encontrado.");
            }
        }
    }
}
