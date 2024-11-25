using PosTech.Entidades;

namespace PosTech.Repository.Interfaces
{
    public interface IContatoRepository : IRepository<Contato>
    {
        IList<Contato> ObterContatosPorRegiao(int regiaoDDD);
    }
}
