using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Interfaces
{
    public interface IContatoService
    {
        IList<Contato> ObterTodos();        
        IList<Contato> ObterContatosPorRegiao(int regiaoDDD);
        Contato ObterPorId(int id);
        string Cadastrar(Contato contato);
        string Alterar(Contato contato);
        string Deletar(int id);
    }
}
