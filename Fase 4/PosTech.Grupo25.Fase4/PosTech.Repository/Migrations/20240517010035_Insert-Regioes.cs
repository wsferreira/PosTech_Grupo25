using Microsoft.EntityFrameworkCore.Migrations;
using PosTech.Entidades;
using System.Collections.Generic;

#nullable disable

namespace PosTech.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InsertRegioes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
            table: "Regiao",
            columns: new[] { "Id", "Estado", "Descricao", "DataCriacao" },
            values: new object[,] {

                            //Região 11
                { "11", "São Paulo", "Região Metropolitana de São Paulo/Região Metropolitana de Jundiaí/Região Geográfica Imediata de Bragança Paulista", DateTime.Now },

                            //Região 12
                { "12", "São Paulo", "Região Metropolitana do Vale do Paraíba e Litoral Norte", DateTime.Now },

                            //Região 13
                { "13", "São Paulo", "Região Metropolitana da Baixada Santista/Vale do Ribeira", DateTime.Now },

                            //Região 14
                { "14", "São Paulo", "Avaré/Bauru/Botucatu/Jaú/Lins/Marília/Ourinhos", DateTime.Now },

                            //Região 15
                { "15", "São Paulo", "Itapetininga/Itapeva/Sorocaba/Tatuí", DateTime.Now },

                            //Região 16
                { "16", "São Paulo", "Araraquara/Franca/Jaboticabal/Ribeirão Preto/São Carlos/Sertãozinho", DateTime.Now },

                            //Região 17
                { "17", "São Paulo", "Barretos/Catanduva/Fernandópolis/Jales/São José do Rio Preto/Votuporanga", DateTime.Now },

                            //Região 18
                { "18", "São Paulo", "Andradina/Araçatuba/Assis/Birigui/Dracena/Presidente Prudente", DateTime.Now },

                            //Região 19
                { "19", "São Paulo", "Americana/Campinas/Limeira/Piracicaba/Rio Claro/São João da Boa Vista", DateTime.Now },

                            //Região 21
                { "21", "Rio de Janeiro", "Rio de Janeiro e Região Metropolitana/Teresópolis", DateTime.Now },

                            //Região 22
                { "22", "Rio de Janeiro", "Cabo Frio/Campos dos Goytacazes/Itaperuna/Macaé/Nova Friburgo", DateTime.Now },

                            //Região 24
                { "24", "Rio de Janeiro", "Angra dos Reis/Petrópolis/Volta Redonda/Piraí", DateTime.Now },

                            //Região 27
                { "27", "Espírito Santo", "Vitória e Região Metropolitana/Colatina/Linhares/Santa Maria de Jetibá", DateTime.Now },

                            //Região 28
                { "28", "Espírito Santo", "Cachoeiro de Itapemirim/Castelo/Itapemirim/Marataízes", DateTime.Now },

                            //Região 31
                { "31", "Minas Gerais", "Belo Horizonte e Região Metropolitana/Conselheiro Lafaiete/Ipatinga/Viçosa", DateTime.Now },

                            //Região 32
                { "32", "Minas Gerais", "Barbacena/Juiz de Fora/Muriaé/São João del-Rei/Ubá", DateTime.Now },

                            //Região 33
                { "33", "Minas Gerais", "Almenara/Caratinga/Governador Valadares/Manhuaçu/Teófilo Otoni", DateTime.Now },

                    // Região 34
                { "34", "Minas Gerais", "Araguari/Araxá/Patos de Minas/Uberlândia/Uberaba", DateTime.Now },

                            //Região 35
                { "35", "Minas Gerais", "Alfenas/Guaxupé/Lavras/Poços de Caldas/Pouso Alegre/Varginha", DateTime.Now },

                            //Região 37
                { "37", "Minas Gerais", "Bom Despacho/Divinópolis/Formiga/Itaúna/Pará de Minas", DateTime.Now },

                            //Região 38
                { "38", "Minas Gerais", "Curvelo/Diamantina/Montes Claros/Pirapora/Unaí", DateTime.Now },

                            //Região 41
                { "41", "Paraná", "Curitiba e Região Metropolitana", DateTime.Now },

                            //Região 42
                { "42", "Paraná", "Ponta Grossa/Guarapuava", DateTime.Now },

                            //Região 43
                { "43", "Paraná", "Apucarana/Londrina", DateTime.Now },

                            //Região 44
                { "44", "Paraná", "Maringá/Campo Mourão/Umuarama", DateTime.Now },

                            //Região 45
                { "45", "Paraná", "Cascavel/Foz do Iguaçu", DateTime.Now },

                            //Região 46
                { "46", "Paraná", "Francisco Beltrão/Pato Branco", DateTime.Now },

                            //Região 47
                { "47", "Santa Catarina", "Balneário Camboriú/Blumenau/Itajaí/Joinville", DateTime.Now },

                            //Região 48
                { "48", "Santa Catarina", "Florianópolis e Região Metropolitana/Criciúma", DateTime.Now },

                            //Região 49
                { "49", "Santa Catarina", "Caçador/Chapecó/Lages", DateTime.Now },

                            //Região 51
                { "51", "Rio Grande do Sul", "Porto Alegre e Região Metropolitana/Santa Cruz do Sul/Litoral Norte", DateTime.Now },

                            //Região 53
                { "53", "Rio Grande do Sul", "Pelotas/Rio Grande", DateTime.Now },

                            //Região 54
                { "54", "Rio Grande do Sul", "Caxias do Sul/Passo Fundo", DateTime.Now },

                            //Região 55
                { "55", "Rio Grande do Sul", "Santa Maria/Santana do Livramento/Santo Ângelo/Uruguaiana", DateTime.Now },

                            //Região 61
                { "61", "Distrito Federal/Goiás", "Abrangência em todo o Distrito Federal e alguns municípios da Região Integrada de Desenvolvimento do Distrito Federal e Entorno", DateTime.Now },

                            //Região 62
                { "62", "Goiás", "Goiânia e Região Metropolitana/Anápolis/Niquelândia/Porangatu", DateTime.Now },

                            //Região 63
                { "63", "Tocantins", "Abrangência em todo o estado", DateTime.Now },

                            //Região 64
                { "64", "Goiás", "Caldas Novas/Catalão/Itumbiara/Rio Verde", DateTime.Now },

                            //Região 65
                { "65", "Mato Grosso", "Cuiabá e Região Metropolitana", DateTime.Now },

                            //Região 66
                { "66", "Mato Grosso", "Rondonópolis/Sinop", DateTime.Now },

                            //Região 67
                { "67", "Mato Grosso do Sul", "Abrangência em todo o estado", DateTime.Now },

                            //Região 68
                { "68", "Acre", "Abrangência em todo o estado", DateTime.Now },

                            //Região 69
                { "69", "Rondônia", "Abrangência em todo o estado", DateTime.Now },

                            //Região 71
                { "71", "Bahia", "Salvador e Região Metropolitana", DateTime.Now },

                            //Região 73
                { "73", "Bahia", "Eunápolis/Ilhéus/Porto Seguro/Teixeira de Freitas", DateTime.Now },

                            //Região 74
                { "74", "Bahia", "Irecê/Jacobina/Juazeiro/Xique-Xique", DateTime.Now },

                            //Região 75
                { "75", "Bahia", "Alagoinhas/Feira de Santana/Paulo Afonso/Valença", DateTime.Now },

                            //Região 77
                { "77", "Bahia", "Barreiras/Bom Jesus da Lapa/Guanambi/Vitória da Conquista", DateTime.Now },

                            //Região 79
                { "79", "Sergipe", "Abrangência em todo o estado", DateTime.Now },

                            //Região 81
                { "81", "Pernambuco", "Recife e Região Metropolitana/Caruaru", DateTime.Now },

                            //Região 82
                { "82", "Alagoas", "Abrangência em todo o estado", DateTime.Now },

                            //Região 83
                { "83", "Paraíba", "Abrangência em todo o estado", DateTime.Now },

                            //Região 84
                { "84", "Rio Grande do Norte", "Abrangência em todo o estado", DateTime.Now },

                            //Região 85
                { "85", "Ceará", "Fortaleza e Região Metropolitana", DateTime.Now },

                            //Região 86
                { "86", "Piauí", "Teresina e alguns municípios da Região Integrada de Desenvolvimento da Grande Teresina/Parnaíba", DateTime.Now },

                            //Região 87
                { "87", "Pernambuco", "Garanhuns/Petrolina/Salgueiro/Serra Talhada", DateTime.Now },

                            //Região 88
                { "88", "Ceará", "Juazeiro do Norte/Sobral", DateTime.Now },

                            //Região 89
                { "89", "Piauí", "Picos/Floriano", DateTime.Now },


                                // Região 91
                { "91", "Pará", "Belém e Região Metropolitana", DateTime.Now },

                            //Região 92
                { "92", "Amazonas", "Manaus e Região Metropolitana/Parintins", DateTime.Now },

                            //Região 93
                { "93", "Pará", "Santarém/Altamira", DateTime.Now },

                            //Região 94
                { "94", "Pará", "Marabá", DateTime.Now },

                            //Região 95
                { "95", "Roraima", "Abrangência em todo o estado", DateTime.Now },

                            //Região 96
                { "96", "Amapá", "Abrangência em todo o estado", DateTime.Now },

                            //Região 97
                { "97", "Amazonas", "Abrangência no interior do estado", DateTime.Now },

                            //Região 98
                { "98", "Maranhão", "São Luís e Região Metropolitana", DateTime.Now },

                            //Região 99
                { "99", "Maranhão", "Caxias/Codó/Imperatriz", DateTime.Now },

            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Regiao", true);
        }
    }
}
