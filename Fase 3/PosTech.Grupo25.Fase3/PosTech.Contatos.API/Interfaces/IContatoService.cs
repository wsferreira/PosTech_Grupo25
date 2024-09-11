using PosTech.Entidades;

namespace PosTech.Contatos.API.Interfaces
{
    public interface IContatoService
    {
        IEnumerable<Contato> ObterTodos();        
        IEnumerable<Contato> ObterContatosPorRegiao(int regiaoDDD);
        Contato ObterPorId(int id);
        /*void Cadastrar(Contato contato);
        void Alterar(Contato contato);
        void Deletar(int id);*/
    }
}
