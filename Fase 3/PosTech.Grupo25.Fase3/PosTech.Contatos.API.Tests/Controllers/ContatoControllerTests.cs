using Microsoft.AspNetCore.Mvc;
using Moq;
using PosTech.Contatos.API.Controllers;
using PosTech.Contatos.API.Interfaces;
using PosTech.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Contatos.API.Tests.Controllers
{
    public class ContatoControllerTests
    {
        [Fact(DisplayName = "Validando o retorno da lista de contatos com sucesso")]
        [Trait("Categoria", "Validando Contato - Lista")]
        public void ObterTodos_DeveRetornarOk200_Sucesso()
        {
            List<Contato> lista = new List<Contato>
            {
                new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 },
                new Contato { Id = 2, Nome = "Maria", Email = "maria@email.com", Telefone = "9875-5678", RegiaoId = 11 },
                new Contato { Id = 3, Nome = "Carlos", Email = "carlos@email.com", Telefone = "9887-5678", RegiaoId = 11 }

            };

            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.ObterTodos())
                .Returns(lista);
            var controller = new ContatoController(mockService.Object);

            // Act
            var result = controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Validando o retorno do contato do contato por região")]
        [Trait("Categoria", "Validando Contato - Lista por Região")]
        public void ObterContatosPorRegiao_DeveRetornarOk200_Sucesso()
        {
            int ID = 11;

            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.ObterContatosPorRegiao(ID));

            var controller = new ContatoController(mockService.Object);

            // Act
            var result = controller.ObterContatosPorRegiao(ID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Validando o retorno os dados do contato por ID")]
        [Trait("Categoria", "Validando Contato - Lista por ID")]
        public void ObterPorId_DeveRetornarOk200_Sucesso()
        {
            int ID = 5;

            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.ObterPorId(ID));

            var controller = new ContatoController(mockService.Object);

            // Act
            var result = controller.ObterPorId(11);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Validando o retorno da inclusão do contato")]
        [Trait("Categoria", "Validando Contato - Inclusão do Contato")]
        public void Cadastrar_DeveRetornarOk200_Sucesso()
        {
            Contato cont = new Contato()
            {
                Id = 0,
                Nome = "Antônio José Lima Junior",
                Email = "ajljunior@gmail.com",
                Telefone = "(11) 97575-0093",
                RegiaoId = 11
            };

            InputContatoCadastrar contato = new InputContatoCadastrar()
            {
                Email = cont.Email,
                Nome = cont.Nome,
                RegiaoId = cont.RegiaoId,
                Telefone = cont.Telefone
            };

            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.Cadastrar(cont));
            var controller = new ContatoController(mockService.Object);

            // Act
            var result = controller.Cadastrar(contato);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact(DisplayName = "Validando o retorno da alteração do dados do contato")]
        [Trait("Categoria", "Validando Contato - Alteração do Contato")]
        public void Alterar_DeveRetornarOk200_Sucesso()
        {
            Contato cont = new Contato()
            {
                Id = 14,
                Nome = "Antônio José Lima Junior",
                Email = "ajljunior@gmail.com",
                Telefone = "(11) 97575-0093",
                RegiaoId = 11
            };

            InputContatoAlterar contato = new InputContatoAlterar()
            {
                Id = cont.Id,
                Email = cont.Email,
                Nome = cont.Nome,
                RegiaoId = cont.RegiaoId,
                Telefone = cont.Telefone
            };

            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.Alterar(cont));
            var controller = new ContatoController(mockService.Object);
            // Act
            var result = controller.Alterar(contato);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Validando o retorno da exclusão dos dados do contato com sucesso")]
        [Trait("Categoria", "Validando Contato - Lista")]
        public void Deletar_DeveRetornarOk200_Sucesso()
        {
            int ID = 5;

            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.Deletar(ID));

            var controller = new ContatoController(mockService.Object);

            // Act
            var result = controller.Deletar(ID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

        }
    }
}
