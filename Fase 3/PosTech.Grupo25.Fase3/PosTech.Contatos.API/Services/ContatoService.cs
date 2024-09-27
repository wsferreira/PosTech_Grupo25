using PosTech.Repository.Interfaces;
using PosTech.Entidades;
using PosTech.Contatos.API.Interfaces;

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
            contato.ValidarEntidade();
            Contato cont = VerificaContatoExiste(contato.Id);
            VerificaRegiaoExiste(contato.RegiaoId);
            cont.Nome = contato.Nome;
            cont.Email = contato.Email;
            cont.Telefone = contato.Telefone;
            cont.RegiaoId = contato.RegiaoId;

            _contatoRepository.Alterar(cont);
        }

        public void Cadastrar(Contato contato)
        {
            contato.ValidarEntidade();
            VerificaRegiaoExiste(contato.RegiaoId);
            _contatoRepository.Cadastrar(contato);
        }
        public void Deletar(int id)
        {
            VerificaContatoExiste(id);
            _contatoRepository.Deletar(id);
        }
        public IEnumerable<Contato> ObterContatosPorRegiao(int regiaoDDD)
        {
            return _contatoRepository.ObterContatosPorRegiao(regiaoDDD);
        }

        public Contato ObterPorId(int id)
        {
            return _contatoRepository.ObterPorId(id);
        }

        public IEnumerable<Contato> ObterTodos()
        {
            return _contatoRepository.ObterTodos();
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
