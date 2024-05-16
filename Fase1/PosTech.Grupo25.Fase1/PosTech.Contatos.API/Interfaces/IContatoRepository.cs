using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Interfaces
{
    public interface IContatoRepository : IRepository<Contato>
    {
        IList<Contato> ObterContatosPorRegiao(int regiaoDDD);
    }
}
