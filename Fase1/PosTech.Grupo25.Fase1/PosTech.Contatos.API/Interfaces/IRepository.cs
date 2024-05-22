using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> ObterTodos();
        T ObterPorId(int id);
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Deletar(int id);
    }
}
