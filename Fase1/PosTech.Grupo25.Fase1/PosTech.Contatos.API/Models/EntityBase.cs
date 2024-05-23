using System.Text.Json.Serialization;

namespace PosTech.Contatos.API.Models
{
    public class EntityBase
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}
