using PosTech.Entidades;

namespace PosTech.Contatos.Cadastro.API.Interfaces
{
    public interface IRegiaoServiceCriacaoProducer
    {
        IEnumerable<Regiao> ObterTodos();
        Regiao ObterPorId(int id);
    }
}
