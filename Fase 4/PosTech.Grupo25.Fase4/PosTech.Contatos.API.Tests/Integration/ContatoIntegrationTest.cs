using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PosTech.Entidades;
using PosTech.Repository;
using Microsoft.Extensions.Configuration;
using PosTech.Contatos.API.Controllers;
using PosTech.Contatos.API.Services;
using PosTech.Contatos.Cadastro.API.Controllers;
using PosTech.Contatos.Cadastro.API.Services;
using PosTech.Contatos.Alteracao.API.Controllers;
using PosTech.Contatos.Alteracao.API.Services;
using PosTech.Contatos.Exclusao.API.Controllers;
using PosTech.Contatos.Exclusao.API.Services;
using PosTech.Cadastro.Consumer.Services;
using PosTech.Alteracao.Consumer.Services;
using PosTech.Exclusao.Consumer.Services;

namespace PosTech.Contatos.API.Tests.Integration
{

    public class ContatoIntegrationTest
    {
        private IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings2.json")
            .Build();

        //API 
        private readonly ContatoRepository _contatoRepository;
        private readonly ContatoController _contatoController;
        private readonly ContatoService _contatoService;
        private readonly RegiaoRepository _regiaoRepository;
        private readonly ApplicationDbContext _context;

        //APIs - RabbitMQ
        //Producers
        private readonly ContatoControllerCriacaoProducer _contatoControllerCriacaoProducer;
        private readonly ContatoControllerAlteracaoProducer _contatoControllerAlteracaoProducer;
        private readonly ContatoControllerExclusaoProducer _contatoControllerExclusaoProducer;
        private readonly ContatoServiceCriacaoProducer _contatoServiceCriacaoProducer;
        private readonly ContatoServiceAlteracaoProducer _contatoServiceAlteracaoProducer;
        private readonly ContatoServiceExclusaoProducer _contatoServiceExclusaoProducer;

        //Consumers
        private readonly ContatoServiceCriacaoConsumer _contatoServiceCriacaoConsumer;
        private readonly ContatoServiceAlteracaoConsumer _contatoServiceAlteracaoConsumer;
        private readonly ContatoServiceExclusaoConsumer _contatoServiceExclusaoConsumer;

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

            _contatoServiceCriacaoProducer = new ContatoServiceCriacaoProducer(_configuration, _regiaoRepository);
            _contatoServiceAlteracaoProducer = new ContatoServiceAlteracaoProducer(_configuration, _contatoRepository, _regiaoRepository);
            _contatoServiceExclusaoProducer = new ContatoServiceExclusaoProducer(_configuration, _contatoRepository);
            _contatoControllerCriacaoProducer = new ContatoControllerCriacaoProducer(_contatoServiceCriacaoProducer);
            _contatoControllerAlteracaoProducer = new ContatoControllerAlteracaoProducer(_contatoServiceAlteracaoProducer);
            _contatoControllerExclusaoProducer = new ContatoControllerExclusaoProducer(_contatoServiceExclusaoProducer);

            _contatoServiceCriacaoConsumer = new ContatoServiceCriacaoConsumer(_contatoRepository);
            _contatoServiceAlteracaoConsumer = new ContatoServiceAlteracaoConsumer(_contatoRepository);
            _contatoServiceExclusaoConsumer = new ContatoServiceExclusaoConsumer(_contatoRepository);


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

        [Fact(DisplayName = "Validando cadastro de Contato - Producer")]
        [Trait("Categoria", "Validando adição Contato")]
        public void AddContato()
        {
            //Arrange
            InputContatoCadastrar contato = new InputContatoCadastrar() 
            { Nome = "Wandersao",Email = "wanderson@hotmail.com",Telefone = "9999-9999", RegiaoId = 11};

            //Act
            //IActionResult result = _contatoControllerCriacaoProducer.Cadastrar(contato);

            ////Assert
            //InputContatoCadastrar resultConverted = (InputContatoCadastrar)((OkObjectResult)result).Value;

            //Assert.Equal(contato.Nome, resultConverted.Nome);
            //Assert.Equal(contato.Email, resultConverted.Email);
            //Assert.Equal(contato.Telefone, resultConverted.Telefone);
            //Assert.Equal(contato.RegiaoId, resultConverted.RegiaoId);
            //Assert.Equal(contato, resultConverted);
        }

        [Fact(DisplayName = "Validando exclusão de Contato - Producer")]
        [Trait("Categoria", "Validando exclusão")]
        public void DeleteContato()
        {
            //Arrange
            Contato contato = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };


            //Act
            //IActionResult result = _contatoControllerExclusaoProducer.Deletar(contato.Id);

            ////Assert
            ////Assert.Null(_context.Contato.FirstOrDefault(c => c.Id == 1));
            //Assert.Contains(contato.Id.ToString(),((OkObjectResult)result).Value.ToString());
        }

        [Fact(DisplayName = "Validando alteração de Contato - Producer")]
        [Trait("Categoria", "Validando Alteração")]
        public void UpdateContato()
        {
            //Arrange
            Contato contato = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };
            InputContatoAlterar contatoAlterado = new InputContatoAlterar { Id = 1, Nome = "Maria", Email = "maria@email.com", Telefone = "1234-5679", RegiaoId = 12 };

            //Act
            //IActionResult result = _contatoControllerAlteracaoProducer.Alterar(contatoAlterado);

            //Assert
            //Contato resultConverted = (Contato)((OkObjectResult)result).Value;

            //Assert.Equal(contato.Id, resultConverted.Id);
            //Assert.NotEqual(contato.Nome, resultConverted.Nome);
            //Assert.NotEqual(contato.Email, resultConverted.Email);
            //Assert.NotEqual(contato.Telefone, resultConverted.Telefone);
            //Assert.NotEqual(contato.RegiaoId, resultConverted.RegiaoId);
            //Assert.NotEqual(contato, resultConverted);
        }

    }
}
