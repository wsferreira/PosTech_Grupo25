using PosTech.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Contatos.API.Tests.Models
{
    public class ContatoTests
    {
        [Fact(DisplayName = "Validando se o contato possui nome vazio")]
        [Trait("Categoria", "Validando Contato")]
        public void Contato_Validate_Name_Empty()
        {
            //Arrange & Act
            Contato contato = new Contato { Id = 1, Nome = "", Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            var result = Assert.Throws<DomainException>(() => contato.ValidarEntidade());

            //Assert
            Assert.Equal("O nome não pode estar vazio!", result.Message);
        }

        [Fact(DisplayName = "Validando tamanho do nome")]
        [Trait("Categoria", "Validando Contato")]
        public void Contato_Validate_Name_Length()
        {
            //Arrange & Act
            Contato contato = new Contato { Id = 1, Nome = "João da Silva jkdfskdj fjs flkdjdklj fkld sjdhfshdjfkljsdkflj dslk fjdklsj lkdsjflksdj flkjsdfkljsflsjfkljdsfkl adbsjdkhasj kdjskah djk sahdjkhsafdjkhs dfjkhdsjkfh sdjfjd lk fjdlksfj dlk jfldksjf kldsjflksdjfkdsjfldj fkljdfkjs",
                Email = "joao@email.com", Telefone = "1234-5678", RegiaoId = 11 };

            var result = Assert.Throws<DomainException>(() => contato.ValidarEntidade());

            //Assert
            Assert.Equal("O nome deve ter até 200 caracteres!", result.Message);
        }

        [Fact(DisplayName = "Validando se o contato possui telefone vazio")]
        [Trait("Categoria", "Validando Contato")]
        public void Contato_Validate_Telefone_Empty()
        {
            //Arrange & Act
            Contato contato = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "", RegiaoId = 11 };

            var result = Assert.Throws<DomainException>(() => contato.ValidarEntidade());

            //Assert
            Assert.Equal("O telefone não pode estar vazio!", result.Message);
        }

        [Fact(DisplayName = "Validando tamanho do telefone")]
        [Trait("Categoria", "Validando Contato")]
        public void Contato_Validate_Telefone_Length()
        {
            //Arrange & Act
            Contato contato = new Contato { Id = 1, Nome = "João", Email = "joao@email.com", Telefone = "21321321354564654654654464", RegiaoId = 11 };

            var result = Assert.Throws<DomainException>(() => contato.ValidarEntidade());

            //Assert
            Assert.Equal("O telefone deve ter até 20 caracteres!", result.Message);
        }

        [Fact(DisplayName = "Validando se o contato possui ne-mailome vazio")]
        [Trait("Categoria", "Validando Contato")]
        public void Contato_Validate_Email_Empty()
        {
            //Arrange & Act
            Contato contato = new Contato { Id = 1, Nome = "João", Email = "", Telefone = "1234-5678", RegiaoId = 11 };

            var result = Assert.Throws<DomainException>(() => contato.ValidarEntidade());

            //Assert
            Assert.Equal("O e-mail não pode estar vazio!", result.Message);
        }

        [Fact(DisplayName = "Validando tamanho do e-mail")]
        [Trait("Categoria", "Validando Contato")]
        public void Contato_Validate_Email_Length()
        {
            //Arrange & Act
            Contato contato = new Contato
            {
                Id = 1,
                Nome = "João",
                Email = "João da Silva jkdfskdj fjs flkdjdklj fkld sjdhfshdjfkljsdkflj dslk fjdklsj lkdsjflksdj flkjsdfkljsflsjfkljdsfkl adbsjdkhasj kdjskah djk sahdjkhsafdjkhs dfjkhdsjkfh sdjfjd lk fjdlksfj dlk jfldksjf kldsjflksdjfkdsjfldj fkljdfkjs",
                Telefone = "1234-5678",
                RegiaoId = 11
            };

            var result = Assert.Throws<DomainException>(() => contato.ValidarEntidade());

            //Assert
            Assert.Equal("O e-mail deve ter até 100 caracteres!", result.Message);
        }

        [Theory(DisplayName = "Validando e-mail válido")]
        [Trait("Categoria", "Validando Contato")]
        [InlineData("abc")]
        [InlineData("@1231")]
        [InlineData("@1231.com")]
        [InlineData("abc.com")]
        public void Contato_Validate_IsEmail(string email)
        {
            //Arrange & Act
            Contato contato = new Contato { Id = 1, Nome = "João", Email = email, Telefone = "2112-4567", RegiaoId = 11 };

            var result = Assert.Throws<DomainException>(() => contato.ValidarEntidade());

            //Assert
            Assert.Equal("O e-mail informado não é um e-mail válido!", result.Message);
        }
    }
}
