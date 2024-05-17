using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Repository
{
    public class ContatoRepository : EFRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IList<Contato> ObterContatosPorRegiao(int regiaoDDD)
        {
            return _context.Contato.Where(c => c.RegiaoId == regiaoDDD).ToList();
        }

       
    }
}
