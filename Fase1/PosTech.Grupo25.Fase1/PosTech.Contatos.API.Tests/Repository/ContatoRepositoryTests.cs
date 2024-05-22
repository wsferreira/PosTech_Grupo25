using Microsoft.EntityFrameworkCore;
using Moq;
using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;
using PosTech.Contatos.API.Repository;
using PosTech.Contatos.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PosTech.Contatos.API.Tests.Repository
{
    public class ContatoRepositoryTests
    {
        private readonly ContatoRepository _contatoRepository;
        private readonly ApplicationDbContext _context;

        public ContatoRepositoryTests()
        {
            //Configuração do EF InMemory
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "teste_postech_fase1")
            .EnableSensitiveDataLogging()
            .Options;            

            _context = new ApplicationDbContext(options);
            _contatoRepository = new ContatoRepository(_context);
        }

        [Fact(DisplayName = "1. Validando se a lista de contatos é retornada com sucesso")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterTodos_DeveRetornarListaDeContatos()
        {
            // Arrange
            List<Contato> lista = new List<Contato>
            {
                new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 },
                new Contato { Id = 2, Nome = "Maria", Email = "maria@email.com", Telefone = "9875-5678", RegiaoId = 22 },
                new Contato { Id = 3, Nome = "Carlos", Email = "carlos@email.com", Telefone = "9887-5678", RegiaoId = 21 },
                new Contato { Id = 4, Nome = "José", Email = "jose@email.com", Telefone = "9887-5678", RegiaoId = 11 }

            };
            _context.Contato.AddRange(lista);
            _context.SaveChanges();


            ContatoRepository contatoRepository = new ContatoRepository(_context);            // Act
            var contatos = contatoRepository.ObterTodos();

            // Assert
            Assert.NotNull(contatos);
        }

        [Fact(DisplayName = "2. Validando se a lista de contatos por região é retornada com apenas mesmo DDD")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterContatosPorRegiao_DeveRetornarListaDeContatosMesmoDDD()
        {
            // Arrange
            var regiaoDDD = 11; // Exemplo de DDD

            List<Contato> lista = new List<Contato>
            {
                new Contato { Id = 7, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 },
                new Contato { Id = 8, Nome = "Maria", Email = "maria@email.com", Telefone = "9875-5678", RegiaoId = 22 },
                new Contato { Id = 9, Nome = "Carlos", Email = "carlos@email.com", Telefone = "9887-5678", RegiaoId = 21 },
                new Contato { Id = 10, Nome = "José", Email = "jose@email.com", Telefone = "9887-5678", RegiaoId = 11 }

            };
            _context.Contato.AddRange(lista);
            _context.SaveChanges();

            // Act
            var contatos = _contatoRepository.ObterContatosPorRegiao(regiaoDDD);
            var todosContatos = _contatoRepository.ObterTodos();

            // Assert
            Assert.NotNull(contatos);
            Assert.Equal(contatos.Count, todosContatos.Where(c => c.RegiaoId == regiaoDDD).ToList().Count); // Verifica se a lista tem todos os contatos com mesmo DDD

        }


        [Fact(DisplayName = "3. Validando o contato por Id retorna o mesmo registro")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterPorId_DeveRetornarContatoExistente()
        {
            // Arrange
            //var mockContatoRepository = new Mock<IContatoRepository>();
            var contatoId = 1;
            var contatoEsperado = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };
            _context.Contato.Add(contatoEsperado);
            _context.SaveChanges();
            //// Configura o comportamento do mock para retornar o contato esperado
            //mockContatoRepository.Setup(repo => repo.ObterPorId(contatoId))
            //    .Returns(contatoEsperado);

            //// Act
            //var contato = mockContatoRepository.Object.ObterPorId(contatoId);
            var contato = _contatoRepository.ObterPorId(contatoId);

            // Assert
            Assert.NotNull(contato);
            Assert.Equal(contatoEsperado.Id, contato.Id);
            Assert.Equal(contatoEsperado.Nome, contato.Nome);
        }

        [Fact(DisplayName = "4. Validando o contato por Id retorna nenhum registro")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterPorId_DeveRetornarNull()
        {
            // Arrange
           
            var contatoId = 0;

            // Act
            var contato = _contatoRepository.ObterPorId(contatoId);

            // Assert
            Assert.Null(contato);
        }


        [Fact(DisplayName = "5. Validando se o cadastro não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Cadastrar_DeveExecutarSemExcecoes()
        {
            // Arrange
            var contatoParaCadastrar = new Contato { Id = 5, Nome = "João 2", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            // Act & Assert
            var exception = Record.Exception(() => _contatoRepository.Cadastrar(contatoParaCadastrar));
            Assert.Null(exception);
        }


        [Fact(DisplayName = "6. Validando se a alteração não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Alterar_DeveExecutarSemExcecoes()
        {

            // Arrange         
            var contatoParaAlterar = new Contato { Id = 2, Nome = "Denise", Email = "denise@email.com", Telefone = "1234-5678", RegiaoId = 11 };
                     

            // Act & Assert
            var exception = Record.Exception(() => _contatoRepository.Alterar(contatoParaAlterar));


            Assert.Null(exception);
        }

        [Fact(DisplayName = "7. Validando se a exclusão não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Deletar_DeveExecutarSemExcecoes()
        {
            // Arrange
            var contatoParaDeletar = 1;

            // Act & Assert
            var exception = Record.Exception(() => _contatoRepository.Deletar(contatoParaDeletar));
            Assert.Null(exception);
        }
    }
}
