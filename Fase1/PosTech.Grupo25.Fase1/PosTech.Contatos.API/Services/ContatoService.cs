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

        public string Alterar(Contato contato)
        {
            var result = VerificaContatoExiste(contato.Id);

            if(!string.IsNullOrEmpty(result))
            {
                return result;
            }

            result = VerificaRegiaoExiste(contato.RegiaoId);

            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }


            _contatoRepository.Alterar(contato);
            return string.Empty;
        }

        public string Cadastrar(Contato contato)
        {
            var result = VerificaRegiaoExiste(contato.RegiaoId);

            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            _contatoRepository.Cadastrar(contato);
            return string.Empty;
        }

        public string Deletar(int id)
        {
            var result = VerificaContatoExiste(id);

            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            _contatoRepository.Deletar(id);
            return string.Empty;
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

        private string VerificaContatoExiste(int id)
        {
            var cont = _contatoRepository.ObterPorId(id);
            if (cont == null)
            {
                return "Contato não encontrado.";
            }
            return string.Empty;
        }

        private string VerificaRegiaoExiste(int id)
        {
            var cont = _regiaoRepository.ObterPorId(id);
            if (cont == null)
            {
                return "DDD não encontrado.";
            }
            return string.Empty;
        }
    }
}
