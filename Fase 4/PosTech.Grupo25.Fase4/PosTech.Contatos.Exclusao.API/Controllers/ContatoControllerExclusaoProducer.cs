using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosTech.Contatos.Exclusao.API.Interfaces;
using PosTech.Entidades;

namespace PosTech.Contatos.Exclusao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoControllerExclusaoProducer : ControllerBase
    {
        private readonly IContatoServiceExclusaoProducer _contatoService;

        public ContatoControllerExclusaoProducer(IContatoServiceExclusaoProducer contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpDelete("Deletar/{id:int}")]
        [AllowAnonymous]
        public IActionResult Deletar(int id)
        {
            try
            {
                _contatoService.Deletar(id);
                return Ok("Registro " + id.ToString() + " excluido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
