using Microsoft.AspNetCore.Mvc;
using Moq;
using PosTech.Contatos.API.Controllers;
using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;

namespace PosTech.Contatos.API.Tests.Services
{
    public class ContatoAPITests
    {


        [Fact(DisplayName = "Validando o retorno da lista de contatos com sucesso")]
        [Trait("Categoria", "Validando Contato - Lista")]
        public void ObterTodos_DeveRetornarOk200_Sucesso()
        {
            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.ObterTodos());

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
                Nome =  cont.Nome,
                RegiaoId=cont.RegiaoId,
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
                Id= cont.Id,
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
