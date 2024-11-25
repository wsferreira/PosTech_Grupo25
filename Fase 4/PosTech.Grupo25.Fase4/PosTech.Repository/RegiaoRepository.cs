using PosTech.Repository.Interfaces;
using PosTech.Entidades;

namespace PosTech.Repository
{
    public class RegiaoRepository : EFRepository<Regiao>, IRegiaoRepository
    {
        public RegiaoRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
