using PosTech.Entidades;

namespace PosTech.Contatos.API.Interfaces
{
    public interface IRegiaoService
    {
        IEnumerable<Regiao> ObterTodos();
        Regiao ObterPorId(int id);
    }
}
