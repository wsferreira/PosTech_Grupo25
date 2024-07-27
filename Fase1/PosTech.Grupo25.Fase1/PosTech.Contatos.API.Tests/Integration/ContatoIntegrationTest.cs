using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PosTech.Contatos.API.Controllers;
using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;
using PosTech.Contatos.API.Repository;
using PosTech.Contatos.API.Services;

namespace PosTech.Contatos.API.Tests.Integration
{

    public class ContatoIntegrationTest
    {
        private readonly ContatoRepository _contatoRepository;
        private readonly ContatoService _contatoService;
        private readonly ContatoController _contatoController;
        private readonly RegiaoRepository _regiaoRepository;
        private readonly ApplicationDbContext _context;

        public ContatoIntegrationTest()
        {
            //Configuração do EF InMemory
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

            _context = new ApplicationDbContext(options);
            _contatoRepository = new ContatoRepository(_context);
            _regiaoRepository = new RegiaoRepository(_context);
            _contatoService = new ContatoService(_contatoRepository, _regiaoRepository);
            _contatoController = new ContatoController(_contatoService);

            //Exemplos de Região
            List<Regiao> list = new List<Regiao>()
            { 
                new Regiao{ Id = 11 ,Estado = "São Paulo", Descricao = "São Paulo", DataCriacao = DateTime.Now, DataUltimaAlteracao = null },
                new Regiao{ Id = 12 ,Estado = "São Paulo", Descricao = "Vale do Paraíba", DataCriacao = DateTime.Now, DataUltimaAlteracao = null },
                new Regiao{ Id = 13 ,Estado = "São Paulo", Descricao = "Eldorado", DataCriacao = DateTime.Now, DataUltimaAlteracao = null },
                new Regiao{ Id = 15 ,Estado = "São Paulo", Descricao = "Sorocaba", DataCriacao = DateTime.Now, DataUltimaAlteracao = null },
            };

            //Exemplos de Contato
            List<Contato> lista = new List<Contato>
            {
                new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 },
                new Contato { Id = 2, Nome = "Maria", Email = "maria@email.com", Telefone = "9875-5678", RegiaoId = 12 },
                new Contato { Id = 3, Nome = "Carlos", Email = "carlos@email.com", Telefone = "9887-5678", RegiaoId = 13 },
                new Contato { Id = 4, Nome = "José", Email = "jose@email.com", Telefone = "9887-5678", RegiaoId = 11 }

            };
            _context.Regiao.AddRange(list);
            _context.Contato.AddRange(lista);        
            _context.SaveChanges();
        }

        [Fact(DisplayName = "Validando busca de Contatos")]
        [Trait("Categoria", "Validando Pesquisa de Contatos")]
        public void BuscaContatos()
        {
            //Arrange
            IEnumerable<Contato> lista = _context.Contato.ToList();

            //Act
            IActionResult result = _contatoController.ObterTodos();

            //Assert
            List<Contato> resultConverted = (List<Contato>)((OkObjectResult)result).Value;
            Assert.Equal(4, resultConverted.Count());
            foreach (Contato contato in lista)
            {
                Contato contatofiltrado = resultConverted.First(c => c.Id == contato.Id);
                Assert.Equal(contato, contatofiltrado);
            }
        }

        [Fact(DisplayName = "Validando busca de Contato")]
        [Trait("Categoria", "Validando Pesquisa de Contato")]
        public void BuscaContatoPorId()
        {
            //Arrange
            Contato contato = _context.Contato.FirstOrDefault(c => c.Id == 2);

            //Act
            IActionResult result = _contatoController.ObterPorId(contato.Id);

            //Assert
            Contato resultConverted = (Contato)((OkObjectResult)result).Value;

            Assert.Equal(_context.Contato.FirstOrDefault(c => c.Id == 2), resultConverted);
        }

        [Fact(DisplayName = "Validando busca de Contato por Região")]
        [Trait("Categoria", "Validando Pesquisa de Contato por Região")]
        public void BuscaContatoPorRegiaoId()
        {
            //Arrange
            List<int> list = new List<int> { 1, 4 };
               
            //Act
            IActionResult result = _contatoController.ObterContatosPorRegiao(11);

            //Assert
            List<Contato> resultConverted = (List<Contato>)((OkObjectResult)result).Value;
            Assert.Equal(2, resultConverted.Count());

            foreach (Contato contato in resultConverted)
            {               
                Assert.Equal(11, contato.RegiaoId);
                Assert.Contains(contato.Id, list);
            }
            
        }

        [Fact(DisplayName = "Validando cadastro de Contato")]
        [Trait("Categoria", "Validando adição Contato")]
        public void AddContato()
        {
            //Arrange
            InputContatoCadastrar contato = new InputContatoCadastrar() 
            { Nome = "Leonardo Zanone",Email = "leozinho25@hotmail.com",Telefone = "9999-9999", RegiaoId = 11};

            //Act
            IActionResult result = _contatoController.Cadastrar(contato);

            //Assert
            InputContatoCadastrar resultConverted = (InputContatoCadastrar)((OkObjectResult)result).Value;

            Assert.Equal(contato.Nome, resultConverted.Nome);
            Assert.Equal(contato.Email, resultConverted.Email);
            Assert.Equal(contato.Telefone, resultConverted.Telefone);
            Assert.Equal(contato.RegiaoId, resultConverted.RegiaoId);
            Assert.Equal(contato, resultConverted);
        }

        [Fact(DisplayName = "Validando exclusão de Contato")]
        [Trait("Categoria", "Validando exclusão")]
        public void DeleteContato()
        {
            //Arrange
            Contato contato = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            //Act
            IActionResult result = _contatoController.Deletar(contato.Id);

            //Assert
            Assert.Null(_context.Contato.FirstOrDefault(c => c.Id == 1));
            Assert.Contains(contato.Id.ToString(),((OkObjectResult)result).Value.ToString());
        }

        [Fact(DisplayName = "Validando alteração de Contato")]
        [Trait("Categoria", "Validando Alteração")]
        public void UpdateContato()
        {
            //Arrange
            Contato contato = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };
            InputContatoAlterar contatoAlterado = new InputContatoAlterar { Id = 1, Nome = "Maria", Email = "maria@email.com", Telefone = "1234-5679", RegiaoId = 12 };

            //Act
            IActionResult result = _contatoController.Alterar(contatoAlterado);

            //Assert
            Contato resultConverted = (Contato)((OkObjectResult)result).Value;

            Assert.Equal(contato.Id, resultConverted.Id);
            Assert.NotEqual(contato.Nome, resultConverted.Nome);
            Assert.NotEqual(contato.Email, resultConverted.Email);
            Assert.NotEqual(contato.Telefone, resultConverted.Telefone);
            Assert.NotEqual(contato.RegiaoId, resultConverted.RegiaoId);
            Assert.NotEqual(contato, resultConverted);
        }

    }
}
