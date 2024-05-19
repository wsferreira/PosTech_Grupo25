using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Interfaces
{
    public interface IRegiaoService
    {
        IList<Regiao> ObterTodos();
        Regiao ObterPorId(int id);
    }
}
