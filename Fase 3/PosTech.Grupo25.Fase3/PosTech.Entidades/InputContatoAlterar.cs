﻿namespace PosTech.Entidades
{
    public class InputContatoAlterar
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required int RegiaoId { get; set; }
    }
}