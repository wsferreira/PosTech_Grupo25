using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosTech.Contatos.Alteracao.API.Interfaces;
using PosTech.Entidades;

namespace PosTech.Contatos.Alteracao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoControllerAlteracaoProducer : ControllerBase
    {
        private readonly IContatoServiceAlteracaoProducer _contatoService;

        public ContatoControllerAlteracaoProducer(IContatoServiceAlteracaoProducer contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpPut("Alterar")]
        [AllowAnonymous]
        public IActionResult Alterar([FromForm] InputContatoAlterar contato)
        {
            try
            {
                Contato cont = new Contato()
                {
                    Id = contato.Id,
                    Nome = contato.Nome,
                    Email = contato.Email,
                    Telefone = contato.Telefone,
                    RegiaoId = contato.RegiaoId
                };

                _contatoService.Alterar(cont);
                return Ok(cont);// Created("", AlterarCadastro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
