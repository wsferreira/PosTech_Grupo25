using System.Text.Json.Serialization;

namespace PosTech.Entidades
{
    public class Contato : EntityBase
    {
        public Contato()
        {            
        }
        public Contato(string nome, string email, string telefone, int regiaoId) { 
            Nome = nome;
            Email = email;
            Telefone = telefone;
            RegiaoId = regiaoId;

            ValidarEntidade();
        }

        [JsonPropertyName ("nome")]
        public required string Nome { get; set; }
        [JsonPropertyName("email")]
        public required string Email { get; set; }
        [JsonPropertyName("telefone")]
        public required string Telefone { get; set; }
        [JsonPropertyName("regiaoid")]
        public required int RegiaoId { get; set; }
        [JsonPropertyName("regiao")]
        public virtual Regiao? Regiao { get; set; }

        public void ValidarEntidade()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome não pode estar vazio!");
            AssertionConcern.AssertArgumentNotEmpty(Email, "O e-mail não pode estar vazio!");
            AssertionConcern.AssertArgumentNotEmpty(Telefone, "O telefone não pode estar vazio!");

            AssertionConcern.AssertArgumentLength(Nome, 200, "O nome deve ter até 200 caracteres!");
            AssertionConcern.AssertArgumentLength(Email, 100, "O e-mail deve ter até 100 caracteres!");
            AssertionConcern.AssertArgumentLength(Telefone, 20, "O telefone deve ter até 20 caracteres!");

            AssertionConcern.AssertArgumentNotEmail(Email, "O e-mail informado não é um e-mail válido!");
        }
    }
}
