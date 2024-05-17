namespace PosTech.Contatos.API.Models
{
    public class EntityBase
    {
        public required int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}
