using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosTech.Contatos.Cadastro.API.Interfaces;
using PosTech.Entidades;

namespace PosTech.Contatos.Cadastro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpPost("Cadastrar")]
        [AllowAnonymous]
        public IActionResult Cadastrar([FromForm] InputContatoCadastrar contato)
        {
            try
            {
                Contato cont = new Contato()
                {
                    Id = 0,
                    Nome = contato.Nome,
                    Email = contato.Email,
                    Telefone = contato.Telefone,
                    RegiaoId = contato.RegiaoId
                };

                _contatoService.Cadastrar(cont);
                return Ok(contato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
