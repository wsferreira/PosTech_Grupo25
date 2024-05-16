namespace PosTech.Contatos.API.Models
{
    public class Contato : EntityBase
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required int RegiaoId { get; set; }
        public virtual Regiao Regiao { get; set; }


    }
}
