using Moq;
using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Contatos.API.Tests.Repository
{
    public class ContatoRepositoryTests
    {
        [Fact(DisplayName = "Validando se a lista de contatos é retornada com sucesso")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterTodos_DeveRetornarListaDeContatos()
        {
            // Arrange
            List<Contato> lista = new List<Contato>
            {
                new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 },
                new Contato { Id = 2, Nome = "Maria", Email = "maria@email.com", Telefone = "9875-5678", RegiaoId = 22 },
                new Contato { Id = 3, Nome = "Carlos", Email = "carlos@email.com", Telefone = "9887-5678", RegiaoId = 21 }

            };

            var mockContatoRepository = new Mock<IContatoRepository>();
            mockContatoRepository.Setup(repo => repo.ObterTodos())
                .Returns(lista);

            // Act
            var contatos = mockContatoRepository.Object.ObterTodos();

            // Assert
            Assert.NotNull(contatos);
        }

        [Fact(DisplayName = "Validando se a lista de contatos por região é retornada com apenas mesmo DDD")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterContatosPorRegiao_DeveRetornarListaDeContatosMesmoDDD()
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var regiaoDDD = 11; // Exemplo de DDD

            List<Contato> lista = new List<Contato>
            {
                new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 },
                new Contato { Id = 2, Nome = "Maria", Email = "maria@email.com", Telefone = "9875-5678", RegiaoId = 11 },
                new Contato { Id = 3, Nome = "Carlos", Email = "carlos@email.com", Telefone = "9887-5678", RegiaoId = 11 }

            };

            // Configura o comportamento do mock para retornar uma lista fictícia de contatos
            mockContatoRepository.Setup(repo => repo.ObterContatosPorRegiao(regiaoDDD))
                .Returns(lista);

            // Act
            var contatos = mockContatoRepository.Object.ObterContatosPorRegiao(regiaoDDD);

            // Assert
            Assert.NotNull(contatos);
            Assert.Equal(contatos.Count, contatos.Where(c => c.RegiaoId == regiaoDDD).ToList().Count); // Verifica se a lista tem todos os contatos com mesmo DDD

        }


        [Fact(DisplayName = "Validando o contato por Id retorna o mesmo registro")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterPorId_DeveRetornarContatoExistente()
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var contatoId = 1;
            var contatoEsperado = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            // Configura o comportamento do mock para retornar o contato esperado
            mockContatoRepository.Setup(repo => repo.ObterPorId(contatoId))
                .Returns(contatoEsperado);

            // Act
            var contato = mockContatoRepository.Object.ObterPorId(contatoId);

            // Assert
            Assert.NotNull(contato);
            Assert.Equal(contatoEsperado.Id, contato.Id);
            Assert.Equal(contatoEsperado.Nome, contato.Nome);
        }

        [Fact(DisplayName = "Validando o contato por Id retorna nenhum registro")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterPorId_DeveRetornarNull()
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var contatoId = 0;
            var contatoEsperado = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };
            contatoEsperado = null;

            // Configura o comportamento do mock para retornar o contato esperado
            mockContatoRepository.Setup(repo => repo.ObterPorId(contatoId))
                .Returns(contatoEsperado);

            // Act
            var contato = mockContatoRepository.Object.ObterPorId(contatoId);

            // Assert
            Assert.Null(contato);
        }


        [Fact(DisplayName = "Validando se o cadastro não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Cadastrar_DeveExecutarSemExcecoes()
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var contatoParaCadastrar = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            // Act & Assert
            var exception = Record.Exception(() => mockContatoRepository.Object.Cadastrar(contatoParaCadastrar));
            Assert.Null(exception);
        }


        [Fact(DisplayName = "Validando se a alteração não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Alterar_DeveExecutarSemExcecoes()
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var contatoParaAlterar = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            // Act & Assert
            var exception = Record.Exception(() => mockContatoRepository.Object.Alterar(contatoParaAlterar));
            Assert.Null(exception);
        }

        [Fact(DisplayName = "Validando se a exclusão não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Deletar_DeveExecutarSemExcecoes()
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var contatoParaDeletar = 1;

            // Act & Assert
            var exception = Record.Exception(() => mockContatoRepository.Object.Deletar(contatoParaDeletar));
            Assert.Null(exception);
        }
    }
}
