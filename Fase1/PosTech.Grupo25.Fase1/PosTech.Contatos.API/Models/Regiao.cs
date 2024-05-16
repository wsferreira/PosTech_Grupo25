namespace PosTech.Contatos.API.Models
{
    public class Regiao : EntityBase
    {
        public required int DDD { get; set; }
        public required string Descricao { get; set; }
        public required string Estado { get; set; }
        public virtual ICollection<Contato> Contatos { get; set; }
    }
}
