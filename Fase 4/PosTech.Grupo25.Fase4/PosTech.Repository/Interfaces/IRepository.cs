
using PosTech.Entidades;

namespace PosTech.Repository.Interfaces
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
