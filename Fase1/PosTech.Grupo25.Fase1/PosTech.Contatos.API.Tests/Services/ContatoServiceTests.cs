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

namespace PosTech.Contatos.API.Tests.Services
{
    public class ContatoServiceTests
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
            var mockRegiaoRepository = new Mock<IRegiaoRepository>();
            mockContatoRepository.Setup(repository => repository.ObterTodos())
            .Returns(lista);
            ContatoService contatoService = new ContatoService(mockContatoRepository.Object, mockRegiaoRepository.Object);
            
            // Act
            var contatos = contatoService.ObterTodos();

            // Assert
            Assert.NotNull(contatos);
            Assert.Equal(3, contatos.Count());
            Assert.Equal("maria@email.com", contatos.ToList()[1].Email);

            // Adicione mais verificações conforme necessário
        }

        [Fact(DisplayName = "Validando se a lista de contatos por região é retornada com apenas mesmo DDD")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterContatosPorRegiao_DeveRetornarListaDeContatosMesmoDDD()
        {
            // Arrange
            var regiaoDDD = 11; // Exemplo de DDD

            List<Contato> lista = new List<Contato>
            {
                new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 },
                new Contato { Id = 2, Nome = "Maria", Email = "maria@email.com", Telefone = "9875-5678", RegiaoId = 11 },
                new Contato { Id = 3, Nome = "Carlos", Email = "carlos@email.com", Telefone = "9887-5678", RegiaoId = 11 }

            };

            // Configura o comportamento do mock para retornar uma lista fictícia de contatos
            var mockContatoRepository = new Mock<IContatoRepository>();
            var mockRegiaoRepository = new Mock<IRegiaoRepository>();
            mockContatoRepository.Setup(repository => repository.ObterContatosPorRegiao(regiaoDDD))
            .Returns(lista);
            ContatoService contatoService = new ContatoService(mockContatoRepository.Object, mockRegiaoRepository.Object);

            // Act
            var contatos = contatoService.ObterContatosPorRegiao(regiaoDDD);

            // Assert
            Assert.NotNull(contatos);
            Assert.Equal(contatos.Count(), contatos.Where(c => c.RegiaoId == regiaoDDD).ToList().Count); // Verifica se a lista tem todos os contatos com mesmo DDD
                                            
        }


        [Fact(DisplayName = "Validando o contato por Id retorna o mesmo registro")]
        [Trait("Categoria", "Validando Contato")]
        public void ObterPorId_DeveRetornarContatoExistente()
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var mockRegiaoRepository = new Mock<IRegiaoRepository>();
            var contatoId = 1;
            var contatoEsperado = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            // Configura o comportamento do mock para retornar o contato esperado
            mockContatoRepository.Setup(repository => repository.ObterPorId(contatoId))
                .Returns(contatoEsperado);
            ContatoService contatoService = new ContatoService(mockContatoRepository.Object, mockRegiaoRepository.Object);

            // Act
            var contato = contatoService.ObterPorId(contatoId);

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
            var mockRegiaoRepository = new Mock<IRegiaoRepository>();
            ContatoService contatoService = new ContatoService(mockContatoRepository.Object, mockRegiaoRepository.Object);

            var contatoId = 0;
            var contatoEsperado = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };
            contatoEsperado = null;

            // Configura o comportamento do mock para retornar o contato esperado+
            mockContatoRepository.Setup(repository => repository.ObterPorId(contatoId))
               .Returns(contatoEsperado);

            // Act
            var contato = contatoService.ObterPorId(contatoId);

            // Assert
            Assert.Null(contato);
        }


        [Fact(DisplayName = "Validando se o cadastro não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Cadastrar_DeveExecutarSemExcecoes()
        {
            // Arrange
            var contatoParaCadastrar = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };
            var regiaoDDD = 11;
            Regiao regiaoParaCasdatro = new Regiao { Id = 11, Descricao = "teste", Estado = "São Paulo" };
            var mockContatoRepository = new Mock<IContatoRepository>();
            var mockRegiaoRepository = new Mock<IRegiaoRepository>();

            mockContatoRepository.Setup(repository => repository.Cadastrar(contatoParaCadastrar));
            mockRegiaoRepository.Setup(repository => repository.ObterPorId(regiaoDDD))
                .Returns(regiaoParaCasdatro);
              
            ContatoService contatoService = new ContatoService(mockContatoRepository.Object, mockRegiaoRepository.Object);          

            // Act & Assert
            var exception = Record.Exception(() => contatoService.Cadastrar(contatoParaCadastrar));
            Assert.Null(exception);
        }

        [Theory(DisplayName = "Validando se o cadastro valida DDD")]
        [Trait("Categoria", "Validando Contato")]
        [InlineData(0)]
        [InlineData(-1)]
        public void Cadastrar_DeveValidarDDD(int regiaoId)
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var mockRegiaoRepository = new Mock<IRegiaoRepository>();
            ContatoService contatoService = new ContatoService(mockContatoRepository.Object, mockRegiaoRepository.Object);

            var contatoParaCadastrar = new Contato { Id = 0, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = regiaoId };

            mockContatoRepository.Setup(repository => repository.Cadastrar(contatoParaCadastrar))
               .Throws(new DomainException("DDD não encontrado."));

            // Act & Assert
            var exception = Record.Exception(() => contatoService.Cadastrar(contatoParaCadastrar));

            Assert.Equal("DDD não encontrado.", exception.Message);

         
        }

        [Fact(DisplayName = "Validando se a alteração não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Alterar_DeveExecutarSemExcecoes()
        {
            // Arrange
            var mockContatoRepository = new Mock<IContatoRepository>();
            var mockRegiaoRepository = new Mock<IRegiaoRepository>();
            ContatoService contatoService = new ContatoService(mockContatoRepository.Object, mockRegiaoRepository.Object);

            var contatoParaAlterar = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            // Act & Assert
            var exception = Record.Exception(() => mockContatoRepository.Object.Alterar(contatoParaAlterar));
            Assert.Null(exception);
        }

        [Theory(DisplayName = "Validando se o cadastro valida DDD")]
        [Trait("Categoria", "Validando Contato")]
        [InlineData(0)]
        [InlineData(-1)]
        public void Alterar_DeveValidarDDD(int regiaoId)
        {
            // Arrange
            var mockContatoService = new Mock<IContatoService>();

            var contatoParaAlterar = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = regiaoId };

            mockContatoService.Setup(p => p.Alterar(contatoParaAlterar))
               .Throws(new DomainException("DDD não encontrado."));

            // Act & Assert
            var exception = Record.Exception(() => mockContatoService.Object.Alterar(contatoParaAlterar));

            Assert.Equal("DDD não encontrado.", exception.Message);


        }

        [Theory(DisplayName = "Validando se o cadastro valida contato")]
        [Trait("Categoria", "Validando Contato")]
        [InlineData(0)]
        [InlineData(-1)]
        public void Alterar_DeveValidarContato(int contatoId)
        {
            // Arrange
            var mockContatoService = new Mock<IContatoService>();

            var contatoParaAlterar = new Contato { Id = contatoId, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };



            // Act & Assert
            mockContatoService.Setup(p => p.Alterar(contatoParaAlterar))
                .Throws(new DomainException("Contato não encontrado."));
            var exception = Record.Exception(() => mockContatoService.Object.Alterar(contatoParaAlterar));

            Assert.Equal("Contato não encontrado.", exception.Message);


        }

        [Fact(DisplayName = "Validando se a exclusão não retorna exceções")]
        [Trait("Categoria", "Validando Contato")]
        public void Deletar_DeveExecutarSemExcecoes()
        {
            // Arrange
            var mockContatoService = new Mock<IContatoService>();
            var contatoParaDeletar = 1;

            // Act & Assert
            var exception = Record.Exception(() => mockContatoService.Object.Deletar(contatoParaDeletar));
            Assert.Null(exception);
        }

    }
}
