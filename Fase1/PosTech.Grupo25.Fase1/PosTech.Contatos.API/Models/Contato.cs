﻿using System.Text.Json.Serialization;

namespace PosTech.Contatos.API.Models
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

            AssertionConcern.AssertArgumentLength(Nome, 200, "O título deve ter até 90 caracteres!");
            AssertionConcern.AssertArgumentLength(Email, 100, "O título deve ter até 90 caracteres!");
            AssertionConcern.AssertArgumentLength(Telefone, 20, "A legenda deve ter até 40 caracteres!");
        }
    }
}
