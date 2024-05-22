using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Interfaces
{
    public interface IRegiaoService
    {
        IEnumerable<Regiao> ObterTodos();
        Regiao ObterPorId(int id);
    }
}
