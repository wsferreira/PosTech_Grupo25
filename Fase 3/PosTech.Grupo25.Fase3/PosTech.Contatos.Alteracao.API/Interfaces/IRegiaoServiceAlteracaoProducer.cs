using PosTech.Entidades;

namespace PosTech.Contatos.Alteracao.API.Interfaces
{
    public interface IRegiaoServiceAlteracaoProducer
    {
        IEnumerable<Regiao> ObterTodos();
        Regiao ObterPorId(int id);
    }
}
