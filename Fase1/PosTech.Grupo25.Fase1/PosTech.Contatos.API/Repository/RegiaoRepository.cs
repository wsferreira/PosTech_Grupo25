using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Repository
{
    public class RegiaoRepository : EFRepository<Regiao>, IRegiaoRepository
    {
        public RegiaoRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
