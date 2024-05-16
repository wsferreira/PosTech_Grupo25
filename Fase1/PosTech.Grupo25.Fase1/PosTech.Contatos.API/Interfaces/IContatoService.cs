using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Interfaces
{
    public interface IContatoService
    {
        IList<Contato> ObterTodos();        
        IList<Contato> ObterContatosPorRegiao(int regiaoDDD);
        Contato ObterPorId(int id);
        void Cadastrar(Contato contato);
        void Alterar(Contato contato);
        void Deletar(int id);
    }
}
