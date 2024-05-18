using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Controllers
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

        [HttpGet("ObterTodos")]
        [AllowAnonymous]
        public IActionResult ObterTodos()
        {
            try
            {
                return Ok(_contatoService.ObterTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("ObterContatosPorRegiao/{regiaoDDD:int}")]
        [AllowAnonymous]
        public IActionResult ObterContatosPorRegiao([FromRoute] int regiaoDDD)
        {
            try
            {
                return Ok(_contatoService.ObterContatosPorRegiao(regiaoDDD));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterPorId/{id:int}")]
        [AllowAnonymous]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                return Ok(_contatoService.ObterPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Cadastrar")]
       [AllowAnonymous]
        public IActionResult Cadastrar(Contato contato)
        {
            try
            {
                _contatoService.Cadastrar(contato);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Alterar")]
        [AllowAnonymous]
        public IActionResult Alterar(Contato contato)
        {
            try
            {
                _contatoService.Alterar(contato);
                return Ok();// Created("", AlterarCadastro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Deletar/{id:int}")]
        [AllowAnonymous]
        public IActionResult Deletar(int id)
        {
            try
            {
                _contatoService.Deletar(id);
                return Ok("Registro "+id.ToString()+" excluido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
